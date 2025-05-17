using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class CategoryViewModel
{
    public ICollection<CategoryBllDto> Categories { get; set; } = default!;
}