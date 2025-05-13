using App.BLL.Contracts;
using App.BLL.DTO;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using App.DTO.V1.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class StockOrderService : BaseService<StockOrderBllDto, StockOrderDalDto, IStockOrderRepository>, IStockOrderService
{

    private readonly IAppUow _uow;
    
    public StockOrderService(
        IAppUow uow, 
        IMapper<StockOrderBllDto, StockOrderDalDto> mapper) : base(uow, uow.StockOrderRepository, mapper)
    {
        _uow = uow;
    }
    
    // TODO PROPER MAPPING
    public async Task<StockOrderBllDto> PlaceStockOrderAsync(CreateStockOrderDto dto)
    {
        var order = new StockOrderDalDto()
        {
            SupplierId = dto.SupplierId,
            WarehouseId = dto.WarehouseId,
            Status = "Done!",
            TotalCost = dto.Products.Sum(i => i.UnitCost * i.Quantity)
        };
        _uow.StockOrderRepository.Add(order);

        foreach (var product in dto.Products)
        {
            var line = new StockOrderItemDalDto()
            {
                StockOrder = order,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                Cost = product.UnitCost * product.Quantity
            };
            
            _uow.StockOrderItemRepository.Add(line);

            var invent = await _uow.InventoryRepository
                .FindByWarehouseIdAndProductIdAsync(order.WarehouseId, product.ProductId);

            if (invent == null)
            {
                _uow.InventoryRepository.Add(new InventoryDalDto() {
                    WarehouseId = order.WarehouseId,
                    ProductId   = product.ProductId,
                    Quantity    = product.Quantity
                });
            }
            else
            {
                invent.Quantity += product.Quantity;
            }
        }
        
        await _uow.SaveChangesAsync();
        return Mapper.Map(order)!;
    }
}