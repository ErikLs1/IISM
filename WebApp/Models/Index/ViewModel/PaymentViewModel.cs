using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class PaymentViewModel
{
    public ICollection<PaymentMvcDto> Payments { get; set; } = default!;
}