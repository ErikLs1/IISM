using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class InventoryViewModel
{
    public ICollection<InventoryMvcDto> Inventories { get; set; } = default!;
}