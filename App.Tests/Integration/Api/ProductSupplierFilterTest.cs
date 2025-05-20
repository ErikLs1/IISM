using System.Net;
using System.Net.Http.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Base.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace App.Tests.Integration.Api;

public class ProductSupplierFilterTest : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string _managerJwt = "";
    private string _userJwt = "";
    
    public ProductSupplierFilterTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = false
        });
    }

    // ================================== Data Initialization====================================
    
    public async Task InitializeAsync()
    {
        _managerJwt = await LoginAsync("manager@test.ee");
        _userJwt = await LoginAsync("user@test.ee");
        await ClearProductSupplierDataAsync();
        await SeedProductSupplierDataAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    // ================================== Helper Functions ====================================
    private async Task<string> LoginAsync(string email)
    {
        var login = new LoginDto { Email = email, Password = "Abc123-" };
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", login);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<JwtResponseDto>())!.Jwt;
    }
    
    private async Task ClearProductSupplierDataAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        ctx.ProductSuppliers.RemoveRange(ctx.ProductSuppliers);
        ctx.Products.RemoveRange(ctx.Products);
        ctx.Suppliers.RemoveRange(ctx.Suppliers);
        ctx.Categories.RemoveRange(ctx.Categories);
        await ctx.SaveChangesAsync();
    }
    
    private async Task SeedProductSupplierDataAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Categories
        var catElectronics = new Category { CategoryName = "Electronics", CategoryDescription = "some description"};
        var catOffice = new Category { CategoryName = "Office", CategoryDescription = "some description" };
        var catSports = new Category { CategoryName = "Sports", CategoryDescription = "some description" };

        ctx.Categories.AddRange(catElectronics, catOffice, catSports);

        // Suppliers
        var suppliers = new[]
        {
            new Supplier { SupplierName = "Globex", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Tallinn", SupplierState = "Harjumaa", SupplierCountry = "EE" , SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email"},
            new Supplier { SupplierName = "Initech", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Keila", SupplierState = "Harjumaa", SupplierCountry = "EE", SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email" },
            new Supplier { SupplierName = "Umbrella", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Tartu", SupplierState = "Tartumaa", SupplierCountry = "EE", SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email" },
            new Supplier { SupplierName = "Soylent", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Riga", SupplierState = "Riga-State", SupplierCountry = "LV", SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email"},
            new Supplier { SupplierName = "Wonka", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Vilnius", SupplierState = "Vilniaus", SupplierCountry = "LT", SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email" },
            new Supplier { SupplierName = "Stark Industries", SupplierAddress = "some address", SupplierStreet = "some street", SupplierCity = "Elva", SupplierState = "Tartumaa", SupplierCountry = "EE", SupplierPostalCode = "0000", SupplierPhoneNumber = "some phone number", SupplierEmail = "some email" },
        };
        ctx.Suppliers.AddRange(suppliers);

        // Products
        var products = new[]
        {
            new Product { ProductName = "4K Monitor", Category = catElectronics, ProductPrice = 329, ProductStatus = "ACTIVE", ProductDescription = "some description"},
            new Product { ProductName = "Laptop Stand", Category = catOffice, ProductPrice =  39, ProductStatus = "ACTIVE", ProductDescription = "some description" },
            new Product { ProductName = "Tennis Racket", Category = catSports, ProductPrice =  89, ProductStatus = "ACTIVE" , ProductDescription = "some description"},
        };
        ctx.Products.AddRange(products);

        await ctx.SaveChangesAsync();

        // Product Suppliers
        foreach (var s in suppliers)
        {
            foreach (var p in products)
            {
                ctx.ProductSuppliers.Add(new ProductSupplier
                {
                    Supplier = s,
                    Product = p,
                    UnitCost = p.ProductPrice * 0.7m 
                });
            }
        }
        await ctx.SaveChangesAsync();
    }
    
    private async Task AssertFilterAsync(
        int page = 1,
        int size = 50,
        string? city = null,
        string? state = null,
        string? country = null,
        string? category = null,
        string? supplier = null,
        int expect = 0)
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        var url =
            $"/api/v1/productsuppliers/getFilteredProductSuppliers" +
            $"?pageIndex={page}&pageSize={size}" +
            $"&city={city}&state={state}&country={country}" +
            $"&category={category}&supplier={supplier}";

        var dto = await _client
            .GetFromJsonAsync<PagedData<ProductSupplierDto>>(url);

        Assert.NotNull(dto);
        Assert.Equal(expect, dto.Items.Count());
        Assert.Equal(expect, dto.TotalCount);
    }
    
    // ================================== Tests ====================================
    
    [Fact]
    public async Task GetFilters_ReturnsDistinctFilters()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        var dto = await _client.GetFromJsonAsync<ProductSupplierFiltersDto>(
            "/api/v1/productSuppliers/getFilters");

        Assert.NotNull(dto);

        Assert.Equal(3, dto.Countries.Count());
        Assert.Equal(4, dto.States.Count());
        Assert.Equal(6, dto.Cities.Count());
        Assert.Equal(3, dto.Categories.Count());
        Assert.Equal(6, dto.Suppliers.Count());
    }
    
    [Fact]
    public async Task GetFilters_Unauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        var res = await _client.GetAsync("/api/v1/productSuppliers/getFilters");
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
    
    [Fact]
    public async Task GetFilters_AuthorizedWithoutManagerRole()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _userJwt);
        var res = await _client.GetAsync("/api/v1/productSuppliers/getFilters");

        Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_ByCity()
    {
        await AssertFilterAsync(city: "Tallinn", expect: 3);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_ByState()
    {
        await AssertFilterAsync(state: "Harjumaa", expect: 6);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_ByCountry()
    {
        await AssertFilterAsync(country: "EE", expect: 12);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_ByCategory()
    {
        await AssertFilterAsync(category: "Electronics", expect: 6);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_BySupplier()
    {
        await AssertFilterAsync(supplier: "Globex", expect: 3);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_ByCountryAndCity()
    {
        await AssertFilterAsync(country: "EE", city: "Keila", expect: 3);
    }
    
    [Fact]
    public async Task GetFilteredProductSuppliers_NoFilters_ReturnsAll()
    {
        await AssertFilterAsync(expect: 18);
    }
}