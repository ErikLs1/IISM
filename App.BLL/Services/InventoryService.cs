using App.BLL.Contracts;
using App.BLL.DTO;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class InventoryService : BaseService<InventoryBllDto, InventoryDalDto, IInventoryRepository>, IInventoryService
{
    private readonly InventoryProductsBllMapper _mapper = new InventoryProductsBllMapper();
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
        return products.Select(e => _mapper.Map(e)!).ToList();
    }

    public async Task<IEnumerable<InventoryProductsBllDto>> GetFilteredInventoryProductsAsync(
        decimal? minPrice, decimal? maxPrice, string? category, string? productName)
    {
        var allProducts = await ServiceRepository.GetFilteredInventoryProductsAsync(
            minPrice, maxPrice, category, productName);
        return allProducts.Select(x => _mapper.Map(x)!);
    }

    public async override Task<IEnumerable<InventoryBllDto>> AllAsync(Guid userId = default)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => Mapper.Map(e)!).ToList();
    }
}