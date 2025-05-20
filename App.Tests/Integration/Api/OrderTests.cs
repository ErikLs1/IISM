using System.Net;
using System.Net.Http.Json;
using App.DAL.EF;
using App.Domain;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Tests.Integration.Api;

public class OrderTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private string _managerJwt = "";
    private string _userJwt = "";
    private Guid _1prodictId;
    private Guid _2productId;
    
    public OrderTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = false
        });
    }

    // ================================== Data Initialization====================================
    private static CreateOrderDto MakeValidOrderDto(Guid prodA, Guid prodB) =>
        new()
        {
            ShippingAddress = "221B Baker St",
            PaymentMethod = "VISA",
            Products = new[]
            {
                new CreateOrderProductDto { ProductId = prodA, Quantity = 2 },
                new CreateOrderProductDto { ProductId = prodB, Quantity = 1 },
            }
        };
    
    public async Task InitializeAsync()
    {
        await ClearDomainAsync();
        await SeedDomainAsync();
        var registrationData = new RegisterDto
        {
            Email       = "bob@test.ee",
            FirstName   = "Bob",
            LastName    = "Somebody",
            Password    = "Abc123-",
            Address     = "123 Main St",
            PhoneNumber = "5553225",
            Gender      = "Male",
            DateOfBirth = new DateOnly(1990, 1, 1),
        };

        var _ = await _client.PostAsJsonAsync("/api/v1/account/register", registrationData);
        
        _userJwt = await LoginAsync("bob@test.ee");
        _managerJwt = await LoginAsync("manager@test.ee");
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
    
    private async Task ClearDomainAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        ctx.OrderProducts.RemoveRange(ctx.OrderProducts);
        ctx.Orders.RemoveRange(ctx.Orders);
        ctx.Payments.RemoveRange(ctx.Payments);
        ctx.Inventories.RemoveRange(ctx.Inventories);
        ctx.ProductSuppliers.RemoveRange(ctx.ProductSuppliers);
        ctx.Products.RemoveRange(ctx.Products);
        ctx.Categories.RemoveRange(ctx.Categories);
        ctx.Warehouses.RemoveRange(ctx.Warehouses);
        await ctx.SaveChangesAsync();
    }
    private async Task SeedDomainAsync()
    {
        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var cat = new Category
        {
            CategoryName = "Sport",
            CategoryDescription = "Sport goods"
        };
        ctx.Categories.Add(cat);

        var prodA = new Product
        {
            CategoryId = cat.Id,
            ProductName = "Dumbbell Set",
            ProductDescription = "Cast iron 10 kg set",
            ProductPrice = 79.99m,
            ProductStatus = "ACTIVE"
        };
        var prodB = new Product
        {
            CategoryId = cat.Id,
            ProductName = "Yoga Mat",
            ProductDescription = "Eco-friendly mat",
            ProductPrice = 24.99m,
            ProductStatus = "ACTIVE"
        };
        ctx.Products.AddRange(prodA, prodB);

        var wh = new Warehouse
        {
            WarehouseAddress = "Central WH",
            WarehouseStreet = "Street 1",
            WarehouseCity = "Tallinn",
            WarehouseState = "Harjumaa",
            WarehouseCountry = "EE",
            WarehouseEmail = "wh@test.ee",
            WarehousePostalCode = "000000",
            WarehouseCapacity = 700
        };
        ctx.Warehouses.Add(wh);

        ctx.Inventories.AddRange(
            new Inventory { Product = prodA, Warehouse = wh, Quantity = 100 },
            new Inventory { Product = prodB, Warehouse = wh, Quantity = 100 });

        await ctx.SaveChangesAsync();

        _1prodictId = prodA.Id;
        _2productId = prodB.Id;
    }
    // ================================== Tests ====================================
    
    [Fact]
    public async Task PlaceOrder_Unauthorized()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        var dto = MakeValidOrderDto(_1prodictId, _2productId);
        var res = await _client.PostAsJsonAsync("/api/v1/orders/placeTheOrder", dto);
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
    
    [Fact]
    public async Task GetFilters_Authorized_Succeed()
    {
        _client.DefaultRequestHeaders.Authorization = new("Bearer", _userJwt);

        var dto  = MakeValidOrderDto(_1prodictId, _2productId);
        var post = await _client.PostAsJsonAsync("/api/v1/orders/placeTheOrder", dto);

        Assert.Equal(HttpStatusCode.Created, post.StatusCode);

        await using var scope = _factory.Services.CreateAsyncScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Assert.Equal(1, await ctx.Orders.CountAsync());
        Assert.Equal(2, await ctx.OrderProducts.CountAsync());
    }

    [Fact]
    public async Task GetUsersOrders_ReturnsOnlyOwnOrders()
    {
        _client.DefaultRequestHeaders.Authorization = new("Bearer", _userJwt);
        await _client.PostAsJsonAsync("/api/v1/orders/placeTheOrder", MakeValidOrderDto(_1prodictId, _2productId));
        await _client.PostAsJsonAsync("/api/v1/orders/placeTheOrder", MakeValidOrderDto(_1prodictId, _2productId));

        _client.DefaultRequestHeaders.Authorization = new("Bearer", _managerJwt);
        await _client.PostAsJsonAsync("/api/v1/orders/placeTheOrder", MakeValidOrderDto(_1prodictId, _2productId));

        _client.DefaultRequestHeaders.Authorization = new("Bearer", _userJwt);
        var res = await _client.GetFromJsonAsync<IEnumerable<UserOrdersDto>>("/api/v1/orders/getUsersOrders");

        Assert.Equal(2, res!.Count());
    }
    
    [Fact]
    public async Task GetAllOrders_NotAllowedForSimpleUser()
    {
        _client.DefaultRequestHeaders.Authorization = new("Bearer", _userJwt);
        var res = await _client.GetAsync("/api/v1/orders/getAllPlacedOrder");
        Assert.Equal(HttpStatusCode.Forbidden, res.StatusCode);
    }
}