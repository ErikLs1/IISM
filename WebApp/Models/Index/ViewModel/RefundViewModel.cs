using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class RefundViewModel
{
    public ICollection<RefundBllDto> Refunds { get; set; } = default!;

}