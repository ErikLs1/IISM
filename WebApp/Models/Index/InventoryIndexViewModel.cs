using App.DAL.DTO;

namespace WebApp.Models.Index;

public class InventoryIndexViewModel
{
    public ICollection<InventoryDalDto> Inventories { get; set; } = default!;
}