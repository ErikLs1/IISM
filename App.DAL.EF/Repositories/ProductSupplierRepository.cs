using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Helpers;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductSupplierRepository : BaseRepository<ProductSupplierDalDto, ProductSupplier>, IProductSupplierRepository
{
    public ProductSupplierRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ProductSupplierUowMapper())
    {
    }

    public async Task<ProductSupplierFiltersDalDto> GetDistinctFiltersAsync()
    {
        var query = GetQuery();
        var cities = await query.Select(ps => ps.Supplier!.SupplierCity).Distinct().OrderBy(x => x).ToArrayAsync();
        var states =  await query.Select(ps => ps.Supplier!.SupplierState).Distinct().OrderBy(x => x).ToArrayAsync();
        var countries =  await query.Select(ps => ps.Supplier!.SupplierCountry).Distinct().OrderBy(x => x).ToArrayAsync();
        var categories =  await query.Select(ps => ps.Product!.Category!.CategoryName).Distinct().OrderBy(x => x).ToArrayAsync();
        var suppliers =  await query.Select(ps => ps.Supplier!.SupplierName).Distinct().OrderBy(x => x).ToArrayAsync();

        return new ProductSupplierFiltersDalDto()
        {
            Cities = cities,
            States = states,
            Countries = countries,
            Categories = categories,
            Suppliers = suppliers
        };
    }

    public async Task<PagedData<ProductSupplierDalDto>> GetPagedDataAsync(int pageIndex, int pageSize, string? city, 
        string? state, string? country, string? category, string? supplier)
    {
        IQueryable<ProductSupplier> query = GetQuery()
            .Include(ps => ps.Supplier)
            .Include(ps => ps.Product)
            .ThenInclude(p => p!.Category);

        if (!string.IsNullOrEmpty(city)) 
            query = query.Where(ps => ps.Supplier!.SupplierCity == city);
        if (!string.IsNullOrEmpty(state)) 
            query = query.Where(ps => ps.Supplier!.SupplierState == state);
        if (!string.IsNullOrEmpty(country)) 
            query = query.Where(ps => ps.Supplier!.SupplierCountry == country);
        if (!string.IsNullOrEmpty(category)) 
            query = query.Where(ps => ps.Product!.Category!.CategoryName == category);
        if (!string.IsNullOrEmpty(supplier)) 
            query = query.Where(ps => ps.Supplier!.SupplierName == supplier);

        var totalCount = await query.CountAsync();

        var pageEntities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dalDto = pageEntities.Select(x => Mapper.Map(x)!);

        return new PagedData<ProductSupplierDalDto>()
        {
            Items = dalDto,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}