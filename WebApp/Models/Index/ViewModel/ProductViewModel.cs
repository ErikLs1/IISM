using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class ProductViewModel
{
    public ICollection<ProductMvcDto> Products { get; set; } = default!;
}