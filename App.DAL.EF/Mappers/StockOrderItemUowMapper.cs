using App.DAL.DTO;
using App.Domain;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class StockOrderItemUowMapper : IMapper<StockOrderItemDalDto, StockOrderItem>
{
    public StockOrderItemDalDto? Map(StockOrderItem? entity)
    {
        if (entity == null) return null;

        var dto = new StockOrderItemDalDto()
        {
            Id = entity.Id,
            StockOrderId = entity.StockOrderId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            Cost = entity.Cost,
            StockOrder = entity.StockOrder == null
                ? null
                : new StockOrderDalDto()
                {
                    Id = entity.StockOrder.Id,
                    SupplierId = entity.StockOrder.SupplierId,
                    WarehouseId = entity.StockOrder.WarehouseId,
                    TotalCost = entity.StockOrder.TotalCost,
                    Status = entity.StockOrder.Status
                },
            Product = entity.Product == null
                ? null
                : new ProductDalDto()
                {
                    Id = entity.Product.Id,
                    CategoryId = entity.Product.CategoryId,
                    ProductName = entity.Product.ProductName,
                    ProductDescription = entity.Product.ProductDescription,
                    ProductPrice = entity.Product.ProductPrice,
                    ProductStatus = entity.Product.ProductStatus
                },
        };

        return dto;
    }

    public StockOrderItem? Map(StockOrderItemDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new StockOrderItem()
        {
            Id = dto.Id,
            StockOrderId = dto.StockOrderId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Cost = dto.Cost,
            StockOrder = dto.StockOrder == null
                ? null
                : new StockOrder()
                {
                    Id = dto.StockOrder.Id,
                    SupplierId = dto.StockOrder.SupplierId,
                    WarehouseId = dto.StockOrder.WarehouseId,
                    TotalCost = dto.StockOrder.TotalCost,
                    Status = dto.StockOrder.Status
                },
            Product = dto.Product == null
                ? null
                : new Product()
                {
                    Id = dto.Product.Id,
                    CategoryId = dto.Product.CategoryId,
                    ProductName = dto.Product.ProductName,
                    ProductDescription = dto.Product.ProductDescription,
                    ProductPrice = dto.Product.ProductPrice,
                    ProductStatus = dto.Product.ProductStatus
                },
        };

        return entity;
    }
}