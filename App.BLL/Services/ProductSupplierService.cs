using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;
using Base.Helpers;

namespace App.BLL.Services;

public class ProductSupplierService : BaseService<ProductSupplierBllDto, ProductSupplierDalDto, IProductSupplierRepository>, IProductSupplierService
{
    public ProductSupplierService(
        IAppUow serviceUow, 
        IMapper<ProductSupplierBllDto, ProductSupplierDalDto> mapper) : base(serviceUow, serviceUow.ProductSupplierRepository, mapper)
    {
    }
    

    public async Task<ProductSupplierFiltersBllDto> GetProductSupplierFilterAsync()
    {
        var filters = await ServiceRepository.GetDistinctFiltersAsync();
        return new ProductSupplierFiltersBllDto()
        {
            Cities = filters.Cities,
            States = filters.States,
            Countries = filters.Countries,
            Categories = filters.Categories,
            Suppliers = filters.Suppliers,
        };
    }

    public async Task<PagedData<ProductSupplierBllDto>> GetPagedDataAsync(
        int pageIndex, int pageSize, string? city, string? state, 
        string? country, string? category, string? supplier)
    {
        var res = await ServiceRepository.GetPagedDataAsync(
            pageIndex, pageSize, city, state, country, category, supplier);

        var bllDto = res.Items.Select(x => Mapper.Map(x)!);
        return new PagedData<ProductSupplierBllDto>()
        {
            Items = bllDto,
            TotalCount = res.TotalCount,
            PageIndex = res.PageIndex,
            PageSize = res.PageSize
        };
    }
}