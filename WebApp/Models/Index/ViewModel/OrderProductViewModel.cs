using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class OrderProductViewModel
{
    public ICollection<OrderProductBllDto> OrderProducts { get; set; } = default!;
 
}