using DTO = LaboRevision.DTO;
using Models = LaboRevision.BLL.Models;

namespace LaboRevision.Mapper;

public static class InvoiceMapper
{
    public static DTO.InvoiceDTO ToDTO(this Models.Invoice model)
    {
        return new DTO.InvoiceDTO
        {
            InvoiceId = model.InvoiceId,
            InvoiceDate = model.InvoiceDate,
            Products = model.Products.Select(p => p.ToDTO())
        };
    }
    
    
}