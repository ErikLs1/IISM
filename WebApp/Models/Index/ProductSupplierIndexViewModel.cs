using App.DAL.DTO;

namespace WebApp.Models.Index;

public class ProductSupplierIndexViewModel
{
    public ICollection<ProductSupplierDalDto> ProductSuppliers { get; set; } = default!;

}