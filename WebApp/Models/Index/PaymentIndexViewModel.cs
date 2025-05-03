using App.DAL.DTO;

namespace WebApp.Models.Index;

public class PaymentIndexViewModel
{
    public ICollection<PaymentDalDto> Payments { get; set; } = default!;

}