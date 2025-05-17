using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class PaymentViewModel
{
    public ICollection<PaymentBllDto> Payments { get; set; } = default!;

}