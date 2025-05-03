using App.DAL.DTO;

namespace WebApp.Models.Index;

public class RefundIndexViewModel
{
    public ICollection<RefundDalDto> Refunds { get; set; } = default!;

}