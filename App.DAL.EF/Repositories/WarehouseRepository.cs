using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WarehouseRepository : BaseRepository<WarehouseDalDto, Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new WarehouseUowMapper())
    {
    }

    public async Task<WarehouseFiltersDalDto> GetDistinctFiltersAsync()
    {
        var query = GetQuery();
        var streets = await query.Select(w => w.WarehouseStreet).Distinct().OrderBy(x => x).ToListAsync();
        var cities = await query.Select(w => w.WarehouseCity).Distinct().OrderBy(x => x).ToListAsync();
        var states = await query.Select(w => w.WarehouseState).Distinct().OrderBy(x => x).ToListAsync();
        var countries = await query.Select(w => w.WarehouseCountry).Distinct().OrderBy(x => x).ToListAsync();

        return new WarehouseFiltersDalDto()
        {
            Streets = streets,
            Cities = cities,
            States = states,
            Countries = countries
        };
    }

    public async Task<IEnumerable<WarehouseDalDto>> GetFilteredWarehousesAsync(
        string? street, 
        string? city, 
        string? state, 
        string? country)
    {
        var query = GetQuery();

        if (!string.IsNullOrEmpty(street)) query = query.Where(w => w.WarehouseStreet == street);
        if (!string.IsNullOrEmpty(city)) query = query.Where(w => w.WarehouseCity == city);
        if (!string.IsNullOrEmpty(state)) query = query.Where(w => w.WarehouseState == state);
        if (!string.IsNullOrEmpty(country)) query = query.Where(w => w.WarehouseCountry == country);

        var list = await query.ToListAsync();

        return list.Select(x => Mapper.Map(x)!);
    }
}