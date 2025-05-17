using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class OrderProductViewModel
{
    public ICollection<OrderProductMvcDto> OrderProducts { get; set; } = default!;
 
}