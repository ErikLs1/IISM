using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class ProductSupplierViewModel
{
    public ICollection<ProductSupplierBllDto> ProductSuppliers { get; set; } = default!;
}