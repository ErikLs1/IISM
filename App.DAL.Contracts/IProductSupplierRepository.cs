using App.DAL.DTO;
using Base.DAL.Contracts;
using Base.Helpers;

namespace App.DAL.Contracts;

public interface IProductSupplierRepository : IBaseRepository<ProductSupplierDalDto>
{
    Task<ProductSupplierFiltersDalDto> GetDistinctFiltersAsync();
    Task<PagedData<ProductSupplierDalDto>> GetPagedDataAsync(
        int pageIndex, int pageSize,
        string? city, string? state, string? country, 
        string? category, string? supplier);
}