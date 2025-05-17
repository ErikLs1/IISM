using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class ProductSupplierViewModel
{
    public ICollection<ProductSupplierMvcDto> ProductSuppliers { get; set; } = default!;
}