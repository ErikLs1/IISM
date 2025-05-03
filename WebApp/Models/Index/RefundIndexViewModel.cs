using App.BLL.DTO;

namespace WebApp.Models.Index;

public class RefundIndexViewModel
{
    public ICollection<RefundBllDto> Refunds { get; set; } = default!;

}