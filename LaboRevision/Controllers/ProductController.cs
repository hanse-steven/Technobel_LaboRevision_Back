using LaboRevision.BLL.Services;
using LaboRevision.DTO;
using LaboRevision.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace LaboRevision.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        try
        {
            IEnumerable<ProductDTO> products = _productService.GetProducts().Select(p => p.ToDTO());
            return Ok(products);
        }
        catch (Exception e)
        {
            return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
