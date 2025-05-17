using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class RefundViewModel
{
    public ICollection<RefundMvcDto> Refunds { get; set; } = default!;

}