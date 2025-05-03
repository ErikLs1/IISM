using App.DAL.DTO;

namespace WebApp.Models.Index;

public class ProductIndexViewModel
{
    public ICollection<ProductDalDto> Products { get; set; } = default!;
}