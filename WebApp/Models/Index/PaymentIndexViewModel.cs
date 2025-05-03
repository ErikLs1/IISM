using App.BLL.DTO;

namespace WebApp.Models.Index;

public class PaymentIndexViewModel
{
    public ICollection<PaymentBllDto> Payments { get; set; } = default!;

}