using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class InventoryService : BaseService<InventoryBllDto, InventoryDalDto, IInventoryRepository>, IInventoryService
{
    public InventoryService(
        IAppUow serviceUow, 
        IMapper<InventoryBllDto, InventoryDalDto> mapper) : base(serviceUow, serviceUow.InventoryRepository, mapper)
    {
    }

    public async Task<IEnumerable<InventoryBllDto>> GetProductsByWarehouseIdAsync(Guid warehouseId)
    {
        var dal = await ServiceRepository.GetProductsByWarehouseIdAsync(warehouseId);
        return dal.Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<InventoryProductsBllDto>> GetAllInventoryProductsAsync()
    {
        var products = await ServiceRepository.GetAllInventoryProductsAsync();
        return products.Select(e => new InventoryProductsBllDto()
        {
            WarehouseId = e.WarehouseId,
            ProductId = e.ProductId,
            ProductName = e.ProductName,
            CategoryName = e.CategoryName,
            ProductPrice = e.ProductPrice,
            WarehouseCity = e.WarehouseCity,
            WarehouseState = e.WarehouseState,
            WarehouseCountry = e.WarehouseCountry,
            ProductDescription = e.ProductDescription
        }).ToList();
    }

    public async Task<IEnumerable<InventoryProductsBllDto>> GetFilteredInventoryProductsAsync(
        decimal? minPrice, decimal? maxPrice, string? category, string? productName)
    {
        // Get all records
        var allProducts = await ServiceRepository.GetAllInventoryProductsAsync();

        // Mapping
        var query = allProducts
            .Select(e => new InventoryProductsBllDto()
            {
                WarehouseId = e.WarehouseId,
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                CategoryName = e.CategoryName,
                ProductPrice = Math.Round(e.ProductPrice * 1.5m, 2),
                WarehouseCity = e.WarehouseCity,
                WarehouseState = e.WarehouseState,
                WarehouseCountry = e.WarehouseCountry,
                ProductDescription = e.ProductDescription
            })
            .AsQueryable();
        
        // Filters
        // TODO - MOVE FILTERING TO REPOSITORY
        if (minPrice.HasValue)
            query = query.Where(x => x.ProductPrice >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(x => x.ProductPrice <= maxPrice.Value);
        
        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => x.CategoryName == category);
        
        if (!string.IsNullOrEmpty(productName))
            query = query.Where(x => x.ProductName
                .Contains(productName, StringComparison.OrdinalIgnoreCase));

        return query.ToList();
    }
}