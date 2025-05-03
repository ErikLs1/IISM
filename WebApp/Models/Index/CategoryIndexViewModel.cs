using App.DAL.DTO;

namespace WebApp.Models.Index;

public class CategoryIndexViewModel
{
    public ICollection<CategoryDalDto> Categories { get; set; } = default!;
}