using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class WarehouseViewModel
{
    public ICollection<WarehouseMvcDto> Warehouses { get; set; } = default!;
}