using App.BLL.DTO;

namespace WebApp.Models.Index;

public class ProductIndexViewModel
{
    public ICollection<ProductBllDto> Products { get; set; } = default!;
}