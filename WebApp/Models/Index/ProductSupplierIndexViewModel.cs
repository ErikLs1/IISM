using App.BLL.DTO;

namespace WebApp.Models.Index;

public class ProductSupplierIndexViewModel
{
    public ICollection<ProductSupplierBllDto> ProductSuppliers { get; set; } = default!;
}