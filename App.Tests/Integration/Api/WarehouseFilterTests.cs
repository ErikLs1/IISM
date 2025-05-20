using System.Net;
using System.Net.Http.Json;
using App.DAL.EF;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace App.Tests.Integration.Api;

public class WarehouseFilterTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string _managerJwt = "";
    private string _userJwt = "";
    
    public WarehouseFilterTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = false
        });
    }
    
    // ================================== Data Initialization====================================
    public async Task InitializeAsync()
    {
        await ClearWarehousesAsync();
        
        _managerJwt = await LoginAsync("manager@test.ee");
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        var seed = new (string City, string State, string Country, int Count)[]
        {
            ("Tallinn",  "Harjumaa", "EE", 3),
            ("Keila",    "Harjumaa", "EE", 2),
            ("Tartu",    "Tartumaa", "EE", 3),
            ("Elva",     "Tartumaa", "EE", 1),
            ("Riga",     "Riga-State", "LV",3),
            ("Vilnius",  "Vilniaus", "LT", 2)
        };

        int id = 1;
        foreach (var (city, state, country, count) in seed)
            for (var i = 0; i < count; i++, id++)
                await CreateWarehouseAsync(
                    address: $"{id} {city} Central St",
                    street: $"Street {id}",
                    city: city,
                    state: state,
                    country: country,
                    postalCode: "00000",
                    email: "wh@example.com",
                    capacity: 100);
        _userJwt = await LoginAsync("user@test.ee");
    }

    public Task DisposeAsync() => Task.CompletedTask;

    
    // ================================== Helper Functions ====================================

    private async Task<Guid> CreateWarehouseAsync(string address, string street, string city, string state,
        string country, string postalCode, string email, int capacity)
    {
        var dto = new WarehouseCreateDto
        {
            WarehouseAddress = address,
            WarehouseStreet = street,
            WarehouseCity = city,
            WarehouseState = state,
            WarehouseCountry = country,
            WarehousePostalCode = postalCode,
            WarehouseEmail = email,
            WarehouseCapacity = capacity,
        };

        var res = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", dto);
        res.EnsureSuccessStatusCode();
        return Guid.Parse(res.Headers.Location!.Segments.Last());
    }

    private async Task<string> LoginAsync(string email)
    {
        var login = new LoginDto { Email = email, Password = "Abc123-" };
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", login);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<JwtResponseDto>())!.Jwt;
    }

    private async Task ClearWarehousesAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        ctx.Warehouses.RemoveRange(ctx.Warehouses);
        await ctx.SaveChangesAsync();
    }

    private async Task<List<WarehouseDto>> GetFilteredWarehousesAsync(string query)
    {
        return await _client.GetFromJsonAsync<List<WarehouseDto>>($"/api/v1/warehouses/getFilteredWarehouses{query}") 
            ?? new ();
    }
    
    // ================================== Tests ====================================
    [Fact]
    public async Task GetFilters_ReturnsDistinctFilters()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var filters = await _client.GetFromJsonAsync<WarehouseFiltersDto>(
            "/api/v1/warehouses/getFilters");

        Assert.NotNull(filters);
        Assert.Equal(3, filters.Countries.Count());
        Assert.Equal(6, filters.Cities.Count());
        Assert.Contains("Tallinn",  filters.Cities);
        Assert.Contains("Harjumaa", filters.States);
        Assert.Contains("EE",       filters.Countries);
    }
    
    [Fact]
    public async Task GetFilters_Unauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        var res = await _client.GetAsync("/api/v1/warehouses/getFilters");
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
    
    [Fact]
    public async Task GetFilters_AuthorizedWithoutManagerRole()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _userJwt);
        var res = await _client.GetAsync("/api/v1/warehouses/getFilters");
        Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
    }
    
    [Fact]
    public async Task GetFilteredWarehouses_ByCity_ReturnsOnlyThatCityWarehouses()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await GetFilteredWarehousesAsync("?city=Tallinn");

        Assert.Equal(3, list.Count);
        Assert.All(list, w => Assert.Equal("Tallinn", w.WarehouseCity));
    }
    
    [Fact]
    public async Task GetFilteredWarehouses_ByCountry_ReturnsOnlyThatCountryWarehouses()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await GetFilteredWarehousesAsync("?state=Tartumaa");

        Assert.Equal(4, list.Count);                
        Assert.All(list, w => Assert.Equal("Tartumaa", w.WarehouseState));
    }
    
    [Fact]
    public async Task GetFilteredWarehouses_ByState_ReturnsOnlyThatStateWarehouses()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await GetFilteredWarehousesAsync("?country=LV");

        Assert.Equal(3, list.Count);                
        Assert.All(list, w => Assert.Equal("LV", w.WarehouseCountry));
    }
    
    [Fact]
    public async Task GetFilteredWarehouses_ByCountryAndCity_ReturnsOnlyThatCountryWarehouses()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await GetFilteredWarehousesAsync("?country=EE&city=Tartu");

        Assert.Equal(3, list.Count);                
        Assert.All(list, w =>
        {
            Assert.Equal("EE",     w.WarehouseCountry);
            Assert.Equal("Tartu",  w.WarehouseCity);
        });
    }
    
    [Fact]
    public async Task GetFilters_NoFilters_ReturnsAllWarehouses()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await GetFilteredWarehousesAsync(string.Empty);

        Assert.Equal(14, list.Count);
    }
}