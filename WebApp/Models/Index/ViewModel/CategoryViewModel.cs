using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class CategoryViewModel
{
    public ICollection<CategoryMvcDto> Categories { get; set; } = default!;
}