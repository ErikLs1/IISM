using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;
using Base.Contracts;

namespace App.BLL.Mappers;

public class ProductBllMapper : IMapper<ProductBllDto, ProductDalDto>
{
    public ProductBllDto? Map(ProductDalDto? entity)
    {
        if (entity == null) return null;

        var dto = new ProductBllDto()
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            ProductName = entity.ProductName,
            ProductDescription = entity.ProductDescription,
            ProductPrice = entity.ProductPrice,
            ProductStatus = entity.ProductStatus,
            Category = entity.Category == null
                ? null
                : new CategoryBllDto()
                {
                    Id = entity.Category.Id,
                    CategoryName = entity.Category.CategoryName,
                    CategoryDescription = entity.Category.CategoryDescription
                },
            ProductSuppliers = entity.ProductSuppliers == null
                ? []
                : entity.ProductSuppliers
                    .Select(o => new ProductSupplierBllDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList(),
            OrderProducts = entity.OrderProducts == null
                ? []
                : entity.OrderProducts
                    .Select(o => new OrderProductBllDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        OrderId = o.OrderId,
                        Quantity = o.Quantity,
                        TotalPrice = o.TotalPrice
                    }).ToList(),
            Inventories = entity.Inventories == null
                ? []
                : entity.Inventories
                    .Select(o => new InventoryBllDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        WarehouseId = o.WarehouseId,
                        Quantity = o.Quantity
                    }).ToList(),
            StockOrderItems = entity.StockOrderItems == null
                ? []
                : entity.StockOrderItems
                    .Select(o => new StockOrderItemBllDto()
                    {
                        Id = o.Id,
                        StockOrderId = o.StockOrderId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                        Cost = o.Cost
                    }).ToList()
        };

        return dto;
    }

    public ProductDalDto? Map(ProductBllDto? dto)
    {
        if (dto == null) return null;

        var entity = new ProductDalDto()
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductStatus = dto.ProductStatus,
            Category = dto.Category == null
                ? null
                : new CategoryDalDto()
                {
                    Id = dto.Category.Id,
                    CategoryName = dto.Category.CategoryName,
                    CategoryDescription = dto.Category.CategoryDescription
                },
        };

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers == null
                ? []
                : dto.ProductSuppliers
                    .Select(o => new ProductSupplierDalDto()
                    {
                        Id = o.Id,
                        SupplierId = o.SupplierId,
                        ProductId = o.ProductId,
                        UnitCost = o.UnitCost
                    }).ToList();
        }
        
        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts == null
                ? []
                : dto.OrderProducts
                    .Select(o => new OrderProductDalDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        OrderId = o.OrderId,
                        Quantity = o.Quantity,
                        TotalPrice = o.TotalPrice
                    }).ToList();
        }
        
        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories == null
                ? []
                : dto.Inventories
                    .Select(o => new InventoryDalDto()
                    {
                        Id = o.Id,
                        ProductId = o.ProductId,
                        WarehouseId = o.WarehouseId,
                        Quantity = o.Quantity
                    }).ToList();
        }
        
        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems == null
                ? []
                : dto.StockOrderItems
                    .Select(o => new StockOrderItemDalDto()
                    {
                        Id = o.Id,
                        StockOrderId = o.StockOrderId,
                        ProductId = o.ProductId,
                        Quantity = o.Quantity,
                        Cost = o.Cost
                    }).ToList();
        }

        return entity;
    }
}