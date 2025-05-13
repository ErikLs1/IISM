using App.BLL.DTO;
using Base.BLL.Contracts;
using Base.Helpers;

namespace App.BLL.Contracts;

public interface IProductSupplierService : IBaseService<ProductSupplierBllDto>
{ 
    Task<ProductSupplierFiltersBllDto> GetProductSupplierFilterAsync();

    Task<PagedData<ProductSupplierBllDto>> GetPagedDataAsync(
        int pageIndex, int pageSize, string? city, 
        string? state, string? country, string? category, string? supplier);
}