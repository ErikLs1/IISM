using System.Net;
using System.Net.Http.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace App.Tests.Integration.Api;

public class InventoryTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string _managerJwt = "";
    private string _userJwt = "";
    private Guid   _warehouseId;
    private Guid   _productId;
    
    public InventoryTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = false
        });
    }

    // ================================== Data Initialization====================================
    
    public async Task InitializeAsync()
    {
        await ClearDomainTablesAsync();
        await SeedDomainDataAsync();

        _managerJwt = await LoginAsync("manager@test.ee");
        _userJwt    = await LoginAsync("user@test.ee");
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
    
    private async Task ClearDomainTablesAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        ctx.Inventories.RemoveRange(ctx.Inventories);
        ctx.Products.RemoveRange(ctx.Products);
        ctx.Categories.RemoveRange(ctx.Categories);
        ctx.ProductSuppliers.RemoveRange(ctx.ProductSuppliers);
        ctx.Warehouses.RemoveRange(ctx.Warehouses);
        await ctx.SaveChangesAsync();
    }
    
    private async Task SeedDomainDataAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var cat = new Category { CategoryName = "Accessories", CategoryDescription = "Misc." };
        var wh  = new Warehouse
        {
            WarehouseAddress    = "1 Supply Rd",
            WarehouseStreet     = "Supply",
            WarehouseCity       = "Tallinn",
            WarehouseState      = "Harjumaa",
            WarehouseCountry    = "EE",
            WarehousePostalCode = "10111",
            WarehouseEmail      = "wh@test.ee",
            WarehouseCapacity   = 50
        };
        var prod = new Product
        {
            Category       = cat,
            ProductName    = "Mouse Pad",
            ProductDescription = "Gaming pad",
            ProductPrice   = 4.99m,
            ProductStatus  = "ACTIVE"
        };
        ctx.Warehouses.Add(wh);
        ctx.Products.Add(prod);
        ctx.Inventories.Add(new Inventory { Warehouse = wh, Product = prod, Quantity = 20 });
        await ctx.SaveChangesAsync();

        _warehouseId = wh.Id;
        _productId   = prod.Id;
    }
    
    // ================================== Tests ====================================
    
    [Fact]
    public async Task GetProductsForWarehouse_Unauthenticated_Returns401()
    {
        var res = await _client.GetAsync(
            $"/api/v1/inventories/getProductsForWarehouse?warehouseId={_warehouseId}");
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
    
    [Fact]
    public async Task GetProductsForWarehouse_WrongRole_Returns403()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _userJwt);

        var res = await _client.GetAsync(
            $"/api/v1/inventories/getProductsForWarehouse?warehouseId={_warehouseId}");
        Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
    }
    
    [Fact]
    public async Task GetProductsForWarehouse_WithManagerRole_Succeed()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var list = await _client.GetFromJsonAsync<IEnumerable<WarehouseInventoryItemsDto>>(
            $"/api/v1/inventories/getProductsForWarehouse?warehouseId={_warehouseId}");

        var item = Assert.Single(list!);
        Assert.Equal(_productId, item.ProductId);
        Assert.Equal(20, item.Quantity);
    }
}