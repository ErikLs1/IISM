using App.BLL.DTO;

namespace WebApp.Models.Index;

public class CategoryIndexViewModel
{
    public ICollection<CategoryBllDto> Categories { get; set; } = default!;
}