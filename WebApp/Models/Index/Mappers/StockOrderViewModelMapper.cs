using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class StockOrderViewModelMapper
{
    public StockOrderMvcDto Map(StockOrderBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new StockOrderMvcDto
        {
            Id = dto.Id,
            TotalCost = dto.TotalCost,
            Status = dto.Status,
            SupplierName = dto.Supplier!.SupplierName,
            WarehouseAdddress = dto.Warehouse!.WarehouseAddress
        };
    }
}