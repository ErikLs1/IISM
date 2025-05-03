using App.BLL.DTO;

namespace WebApp.Models.Index;

public class InventoryIndexViewModel
{
    public ICollection<InventoryBllDto> Inventories { get; set; } = default!;
}