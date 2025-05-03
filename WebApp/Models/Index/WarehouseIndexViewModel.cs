using App.BLL.DTO;

namespace WebApp.Models.Index;

public class WarehouseIndexViewModel
{
    public ICollection<WarehouseBllDto> Warehouses { get; set; } = default!;
}