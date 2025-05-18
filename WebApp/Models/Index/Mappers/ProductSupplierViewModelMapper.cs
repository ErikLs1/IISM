using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class ProductSupplierViewModelMapper
{
    public ProductSupplierMvcDto Map(ProductSupplierBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new ProductSupplierMvcDto
        {
            Id = dto.Id,
            UnitCost = dto.UnitCost,
            SupplierName = dto.Supplier!.SupplierName,
            ProductName = dto.Product!.ProductName
        };
    }
    
    // public ProductSupplierBllDto Map(ProductSupplierMvcDto dto)
    // {
    //     if (dto is null)
    //         throw new ArgumentNullException(nameof(dto));
    //     
    //     return new ProductSupplierBllDto
    //     {
    //         Id = dto.Id,
    //         UnitCost = dto.UnitCost,
    //         SupplierName = dto.Supplier!.SupplierName,
    //         ProductName = dto.Product!.ProductName
    //     };
    // }
}