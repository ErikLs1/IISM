using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Tests.Integration.Api;

public class WarehouseTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private string _userJwt = "";
    private string _managerJwt = "";

    public WarehouseTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = false
        });
    }

    public async Task InitializeAsync()
    {
        // Register test user
        var testUser = new RegisterDto
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
        await _client.PostAsJsonAsync("/api/v1/account/register", testUser);
        
        // Login test user
        var testUserLogin = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };
        var testUserResponse = await _client.PostAsJsonAsync("/api/v1/account/login", testUserLogin);
        testUserResponse.EnsureSuccessStatusCode();
        var testUserResponseData = await testUserResponse.Content.ReadFromJsonAsync<JwtResponseDto>();
        _userJwt = testUserResponseData!.Jwt;
        
        // Get manager JWT
        var mgrLogin = new LoginDto {
            Email    = "manager@test.ee",
            Password = "Abc123-"
        };
        var managerResponse = await _client.PostAsJsonAsync("/api/v1/account/login", mgrLogin);
        managerResponse.EnsureSuccessStatusCode();
        var managerResponseData = await managerResponse.Content.ReadFromJsonAsync<JwtResponseDto>();
        _managerJwt = managerResponseData!.Jwt;
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    [Fact]
    public async Task GetWarehouses_Unauthorized()
    {
        var result = await _client.GetAsync("/api/v1/warehouses/getWarehouses");
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }
    
    [Fact]
    public async Task GetWarehouses_UserWithoutManagerRole()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _userJwt);
        var result = await _client.GetAsync("/api/v1/warehouses/getWarehouses");
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }
    
    [Fact]
    public async Task Manager_CreateWarehouse()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        // Test data
        var warehouseCreate = new WarehouseCreateDto()
        {
            WarehouseAddress = "Some street",
            WarehouseStreet = "Suite 100",
            WarehouseCity = "Metropolis",
            WarehouseState = "NY",
            WarehouseCountry = "USA",
            WarehousePostalCode= "12345",
            WarehouseEmail = "some@email.com",
            WarehouseCapacity = 100
        };
        
        // Create warehouse
        var result = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", warehouseCreate);
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }
    
    [Fact]
    public async Task Manager_GetWarehouseById()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        // Test data
        var warehouseCreate = new WarehouseCreateDto()
        {
            WarehouseAddress = "Some street",
            WarehouseStreet = "Suite 100",
            WarehouseCity = "Metropolis",
            WarehouseState = "NY",
            WarehouseCountry = "USA",
            WarehousePostalCode= "12345",
            WarehouseEmail = "some@email.com",
            WarehouseCapacity = 100
        };
        
        // Create warehouse
        var post = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", warehouseCreate);
        post.EnsureSuccessStatusCode();
        var location = post.Headers.Location!.ToString();
        var id = Guid.Parse(location.Split('/').Last());

        // Get warehouse
        var get = await _client.GetAsync($"/api/v1/warehouses/getWarehouse/{id}");
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);

        // Confirm correct data
        var responseDto = await get.Content.ReadFromJsonAsync<WarehouseDto>();
        Assert.Equal(warehouseCreate.WarehouseAddress, responseDto!.WarehouseAddress);
        Assert.Equal(warehouseCreate.WarehouseEmail, responseDto.WarehouseEmail);
        Assert.Equal(warehouseCreate.WarehouseCapacity, responseDto.WarehouseCapacity);
    }
    
    [Fact]
    public async Task Manager_UpdateWarehouse()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        // Test data
        var warehouseCreate = new WarehouseCreateDto()
        {
            WarehouseAddress = "Some street",
            WarehouseStreet = "Suite 100",
            WarehouseCity = "Metropolis",
            WarehouseState = "NY",
            WarehouseCountry = "USA",
            WarehousePostalCode= "12345",
            WarehouseEmail = "some@email.com",
            WarehouseCapacity = 100
        };
        
        // Create warehouse
        var post = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", warehouseCreate);
        post.EnsureSuccessStatusCode();
        var location = post.Headers.Location!.ToString();
        var id = Guid.Parse(location.Split('/').Last());

        // Get warehouse to update
        var get = await _client.GetFromJsonAsync<WarehouseDto>($"/api/v1/warehouses/getWarehouse/{id}");
        get!.WarehouseCapacity = 300;
        get.WarehouseAddress = "other address";
        
        // Put updated warehouse
        var put = await _client.PutAsJsonAsync($"/api/v1/warehouses/updateWarehouse/{id}", get);
        Assert.Equal(HttpStatusCode.NoContent, put.StatusCode);
        
        // Confirm update
        var updated = await _client.GetFromJsonAsync<WarehouseDto>($"/api/v1/warehouses/getWarehouse/{id}");
        Assert.Equal(300, updated!.WarehouseCapacity);
        Assert.Equal("other address", updated.WarehouseAddress);
    }
    
    [Fact]
    public async Task Manager_DeleteWarehouse()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        // Test data
        var warehouseCreate = new WarehouseCreateDto()
        {
            WarehouseAddress = "Some street",
            WarehouseStreet = "Suite 100",
            WarehouseCity = "Metropolis",
            WarehouseState = "NY",
            WarehouseCountry = "USA",
            WarehousePostalCode= "12345",
            WarehouseEmail = "some@email.com",
            WarehouseCapacity = 100
        };
        
        // Create warehouse
        var post = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", warehouseCreate);
        post.EnsureSuccessStatusCode();
        var location = post.Headers.Location!.ToString();
        var id = Guid.Parse(location.Split('/').Last());

        // Delete warehouse
        var del = await _client.DeleteAsync($"/api/v1/warehouses/deleteWarehouse/{id}");
        Assert.Equal(HttpStatusCode.NoContent, del.StatusCode);
        
        // Confirm delete
        var confirm = await _client.GetAsync($"/api/v1/warehouses/getWarehouse/{id}");
        Assert.Equal(HttpStatusCode.NotFound, confirm.StatusCode);
    }
    
    [Fact]
    public async Task Manager_CanDoAllCRUD()
    {
        _client.DefaultRequestHeaders.Authorization = 
            new ("Bearer", _managerJwt);

        // Test data
        var warehouseCreate = new WarehouseCreateDto()
        {
            WarehouseAddress = "Some street",
            WarehouseStreet = "Suite 100",
            WarehouseCity = "Metropolis",
            WarehouseState = "NY",
            WarehouseCountry = "USA",
            WarehousePostalCode= "12345",
            WarehouseEmail = "some@email.com",
            WarehouseCapacity = 100
        };
        
        // Create warehouse
        var post = await _client.PostAsJsonAsync("/api/v1/warehouses/createWarehouse", warehouseCreate);
        post.EnsureSuccessStatusCode();
        var location = post.Headers.Location!.ToString();
        var id = Guid.Parse(location.Split('/').Last());
        
        // Get created warehouse
        var read = await _client.GetFromJsonAsync<WarehouseDto>($"/api/v1/warehouses/getWarehouse/{id}");
        Assert.Equal(warehouseCreate.WarehouseAddress, read!.WarehouseAddress);

        // Update warehouse
        read.WarehouseCapacity = 400;
        var put = await _client.PutAsJsonAsync($"/api/v1/warehouses/updateWarehouse/{id}", read);
        Assert.Equal(HttpStatusCode.NoContent, put.StatusCode);

        // Delete warehouse
        var delete = await _client.DeleteAsync($"/api/v1/warehouses/deleteWarehouse/{id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);

        
        var confirm = await _client.GetAsync($"/api/v1/warehouses/getWarehouse/{id}");
        Assert.Equal(HttpStatusCode.NotFound, confirm.StatusCode);
    }
}