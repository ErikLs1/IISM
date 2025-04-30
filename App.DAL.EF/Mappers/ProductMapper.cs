using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class ProductMapper : IMapper<ProductDto, Product>
{
    private readonly CategoryMapper _categoryMapper;
    private readonly ProductSupplierMapper _productSupplierMapper;
    private readonly OrderProductMapper _orderProductMapper;
    private readonly InventoryMapper _inventoryMapper;
    private readonly StockOrderItemMapper _stockOrderItemMapper;

    public ProductMapper(CategoryMapper categoryMapper, ProductSupplierMapper productSupplierMapper, OrderProductMapper orderProductMapper, InventoryMapper inventoryMapper, StockOrderItemMapper stockOrderItemMapper)
    {
        _categoryMapper = categoryMapper;
        _productSupplierMapper = productSupplierMapper;
        _orderProductMapper = orderProductMapper;
        _inventoryMapper = inventoryMapper;
        _stockOrderItemMapper = stockOrderItemMapper;
    }

    public ProductDto? Map(Product? entity)
    {
        if (entity == null) return null;

        var dto = new ProductDto()
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            ProductName = entity.ProductName,
            ProductDescription = entity.ProductDescription,
            ProductPrice = entity.ProductPrice,
            ProductStatus = entity.ProductStatus,
            Category = _categoryMapper.Map(entity.Category),
            ProductSuppliers = entity.ProductSuppliers?
                .Select(o => _productSupplierMapper.Map(o)!)
                .ToList(),
            OrderProducts = entity.OrderProducts?
                .Select(o => _orderProductMapper.Map(o)!)
                .ToList(),
            Inventories = entity.Inventories?
                .Select(o => _inventoryMapper.Map(o)!)
                .ToList(),
            StockOrderItems = entity.StockOrderItems?
                .Select(o => _stockOrderItemMapper.Map(o)!)
                .ToList(),
        };

        return dto;
    }

    public Product? Map(ProductDto? dto)
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
            Category = _categoryMapper.Map(dto.Category),
        };

        if (dto.ProductSuppliers != null)
        {
            entity.ProductSuppliers = dto.ProductSuppliers?
                .Select(o => _productSupplierMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.OrderProducts != null)
        {
            entity.OrderProducts = dto.OrderProducts?
                .Select(o => _orderProductMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.Inventories != null)
        {
            entity.Inventories = dto.Inventories?
                .Select(o => _inventoryMapper.Map(o)!)
                .ToList();
        }
        
        if (dto.StockOrderItems != null)
        {
            entity.StockOrderItems = dto.StockOrderItems?
                .Select(o => _stockOrderItemMapper.Map(o)!)
                .ToList();
        }

        return entity;
    }
}