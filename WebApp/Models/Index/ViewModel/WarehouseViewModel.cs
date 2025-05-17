using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class WarehouseViewModel
{
    public ICollection<WarehouseBllDto> Warehouses { get; set; } = default!;
}