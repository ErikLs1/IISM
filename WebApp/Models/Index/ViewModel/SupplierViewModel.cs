using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class SupplierViewModel
{
    public ICollection<SupplierMvcDto> Suppliers { get; set; } = default!;

}