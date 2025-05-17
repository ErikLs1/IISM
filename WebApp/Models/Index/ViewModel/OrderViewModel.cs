using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class OrderViewModel
{
    public ICollection<OrderBllDto> Orders { get; set; } = default!;
}