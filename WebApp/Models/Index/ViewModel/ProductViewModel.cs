using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class ProductViewModel
{
    public ICollection<ProductBllDto> Products { get; set; } = default!;
}