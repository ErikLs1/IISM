using System.Net;
using System.Net.Http.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace App.Tests.Integration.Api;

public class StockOrderTest : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string _managerJwt = "";
    private string _userJwt = "";
    
    public StockOrderTest(CustomWebApplicationFactory<Program> factory)
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
        _userJwt = await LoginAsync("user@test.ee");
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    // ================================== Helper Functions ====================================
    
    private Guid _supplierId;
    private Guid _productId;
    private Guid _warehouseId;

    private CreateStockOrderDto MakeValidOrderDto() =>
        new()
        {
            SupplierId = _supplierId,
            WarehouseId = _warehouseId,
            Products = new()
            {
                new StockOrderItemDto
                {
                    ProductId = _productId,
                    Quantity = 5,
                    UnitCost = 19.95m
                }
            }
        };
    
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

        ctx.StockOrderItems.RemoveRange(ctx.StockOrderItems);
        ctx.StockOrders.RemoveRange(ctx.StockOrders);
        ctx.ProductSuppliers.RemoveRange(ctx.ProductSuppliers);
        ctx.Products.RemoveRange(ctx.Products);
        ctx.Suppliers.RemoveRange(ctx.Suppliers);
        ctx.Warehouses.RemoveRange(ctx.Warehouses);

        await ctx.SaveChangesAsync();
    }

    private async Task SeedDomainDataAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var cat = new Category
        {
            CategoryName        = "Boards",
            CategoryDescription = "All kinds of boards"
        };
        ctx.Categories.Add(cat);
        
        // Create supplier
        var supplier = new Supplier
        {
            SupplierName = "Acme Supplies",
            SupplierAddress = "Acme Street 1",
            SupplierStreet = "Street",
            SupplierCity = "Tallinn",
            SupplierState = "Harjumaa",
            SupplierCountry  = "EE",
            SupplierPostalCode = "10111",
            SupplierEmail = "info@acme.ee",
            SupplierPhoneNumber = "+3726001010"
        };
        ctx.Suppliers.Add(supplier);

        // Create products
        var product = new Product
        {
            CategoryId = cat.Id,
            ProductName = "Foam Board",
            ProductDescription = "Durable foam board",
            ProductPrice = 10m,
            ProductStatus = "ACTIVE"
        };
        ctx.Products.Add(product);

        // Create product supplier
        ctx.ProductSuppliers.Add(new ProductSupplier
        {
            Supplier = supplier,
            Product = product,
            UnitCost = 5m
        });

        // Create warehouse
        var wh = new Warehouse
        {
            WarehouseAddress = "Central 1",
            WarehouseStreet = "Street",
            WarehouseCity = "Tallinn",
            WarehouseState = "Harjumaa",
            WarehouseCountry = "EE",
            WarehousePostalCode = "10111",
            WarehouseEmail = "wh@acme.ee",
            WarehouseCapacity = 1000
        };
        ctx.Warehouses.Add(wh);

        await ctx.SaveChangesAsync();

        // Get PK for DTO
        _supplierId = supplier.Id;
        _productId = product.Id;
        _warehouseId = wh.Id;
    }
    
    // ================================== Tests ====================================
    
    [Fact]
    public async Task PlaceStockOrder_Unauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        
        var dto = MakeValidOrderDto();
        var res = await _client.PostAsJsonAsync("/api/v1/stockOrders/placeStockOrder", dto);

        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
    
    [Fact]
    public async Task PlaceStockOrder_ForbiddenForSimpleUser()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _userJwt);

        var dto = MakeValidOrderDto();
        var res = await _client.PostAsJsonAsync("/api/v1/stockOrders/placeStockOrder", dto);

        Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
    }
    
    [Fact]
    public async Task PlaceStockOrder_BadRequest_MissingFields()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var badDto = new CreateStockOrderDto
        {
            SupplierId  = _supplierId,
            WarehouseId = _warehouseId,
            Products    = new() 
        };

        var res = await _client.PostAsJsonAsync("/api/v1/stockOrders/placeStockOrder", badDto);
        Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
    }
    
    [Fact]
    public async Task PlaceStockOrder_Succeed_Manager()
    {
        _client.DefaultRequestHeaders.Authorization = new ("Bearer", _managerJwt);

        var dto  = MakeValidOrderDto();
        var post = await _client.PostAsJsonAsync("/api/v1/stockOrders/placeStockOrder", dto);

        Assert.Equal(HttpStatusCode.Created, post.StatusCode);
    }
}