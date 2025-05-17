using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class OrderViewModel
{
    public ICollection<OrderMvcDto> Orders { get; set; } = default!;
}