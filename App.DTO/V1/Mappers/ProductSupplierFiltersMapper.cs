using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class ProductSupplierFiltersMapper
{
    public ProductSupplierFiltersDto? Map(ProductSupplierFiltersBllDto? entity)
    {
        if (entity == null) return null;

        var res = new ProductSupplierFiltersDto()
        {
            States = entity.States,
            Cities = entity.Cities,
            Countries = entity.Countries,
            Categories = entity.Categories,
            Suppliers = entity.Suppliers
        };
        return res;
    }
}