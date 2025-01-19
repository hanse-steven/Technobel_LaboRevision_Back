using System.Security.Claims;
using LaboRevision.BLL.Services;
using LaboRevision.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaboRevision.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _invoiceService;
    public InvoiceController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }


    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            string email = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return this.Ok(this._invoiceService.GetByEmail(email).Select(i => i.ToDTO()));
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
