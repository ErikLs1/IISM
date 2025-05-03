using App.DAL.DTO;

namespace WebApp.Models.Index;

public class WarehouseIndexViewModel
{
    public ICollection<WarehouseDalDto> Warehouses { get; set; } = default!;
}