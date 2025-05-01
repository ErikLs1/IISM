using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductUowMapper : IUowMapper<ProductDalDto, Product>
{
    private readonly CategoryUowMapper _categoryUowMapper;
    private readonly ProductSupplierUowMapper _productSupplierUowMapper;
    private readonly OrderProductUowMapper _orderProductUowMapper;
    private readonly InventoryUowMapper _inventoryUowMapper;
    private readonly StockOrderItemUowMapper _stockOrderItemUowMapper;

    public ProductUowMapper(CategoryUowMapper categoryUowMapper, ProductSupplierUowMapper productSupplierUowMapper, OrderProductUowMapper orderProductUowMapper, InventoryUowMapper inventoryUowMapper, StockOrderItemUowMapper stockOrderItemUowMapper)
    {
        _categoryUowMapper = categoryUowMapper;
        _productSupplierUowMapper = productSupplierUowMapper;
        _orderProductUowMapper = orderProductUowMapper;
        _inventoryUowMapper = inventoryUowMapper;
        _stockOrderItemUowMapper = stockOrderItemUowMapper;
    }

    public ProductDalDto? Map(Product? entity)
    {
        if (entity == null) return null;

        var dto = new ProductDalDto()
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            ProductName = entity.ProductName,
            ProductDescription = entity.ProductDescription,
            ProductPrice = entity.ProductPrice,
            ProductStatus = entity.ProductStatus,
            Category = _categoryUowMapper.Map(entity.Category),
            ProductSuppliers = entity.ProductSuppliers?
                .Select(o => _productSupplierUowMapper.Map(o)!)
                .ToList(),
            OrderProducts = entity.OrderProducts?
                .Select(o => _orderProductUowMapper.Map(o)!)
                .ToList(),
            Inventories = entity.Inventories?
                .Select(o => _inventoryUowMapper.Map(o)!)
                .ToList(),
            StockOrderItems = entity.StockOrderItems?
                .Select(o => _stockOrderItemUowMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Product? Map(ProductDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Product()
        {
            Id = dto.Id,
            CategoryId = dto.CategoryId,
            ProductName = dto.ProductName,
            ProductDescription = dto.ProductDescription,
            ProductPrice = dto.ProductPrice,
            ProductStatus = dto.ProductStatus,
            Category = _categoryUowMapper.Map(dto.Category),
        };

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers?
                .Select(o => _productSupplierUowMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts?
                .Select(o => _orderProductUowMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories?
                .Select(o => _inventoryUowMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems?
                .Select(o => _stockOrderItemUowMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}