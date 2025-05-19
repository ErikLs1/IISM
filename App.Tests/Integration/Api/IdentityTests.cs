using System.Net;
using System.Net.Http.Json;
using App.DTO.Identity;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Tests.Integration.Api;

public class IdentityTests : IClassFixture<CustomWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;


    public IdentityTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    public async Task InitializeAsync()
    {
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
    }

    public Task DisposeAsync() => Task.CompletedTask;
    
    
    [Fact]
    public async Task Register_User()
    {
        // Arrange
        var registrationData = new RegisterDto()
        {
            Email = "test@test.ee",
            FirstName = "Test",
            LastName = "User",
            Password = "Abc123-",
            Address = "123 Main St",
            PhoneNumber = "555325",
            Gender = "Male",
            DateOfBirth = new DateOnly(1990,1,1),
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/register", registrationData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }
    
    [Fact]
    public async Task Login_Existing_User()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
    }
    
    [Fact]
    public async Task Login_Existing_User_Check_Rights()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.Jwt);
        
        var getResponse = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        getResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task No_Bearer_Header_Unauthorized()
    {
        var getResponse = await _client.GetAsync("/api/v1/orders/getUsersOrders");
        Assert.Equal(HttpStatusCode.Unauthorized, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task JWT_Custom_Expiration()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=2", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.Jwt);
        
        var getResponse = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        getResponse.EnsureSuccessStatusCode();

        
        // Wait for JWT to expire
        await Task.Delay(3000);
        var getResponseAuthExpired = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        
        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);
    }


    [Fact]
    public async Task JWT_Refresh()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login?jwtExpiresInSeconds=2", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.Jwt);
        
        var getResponse = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        getResponse.EnsureSuccessStatusCode();

        
        // Wait for JWT to expire
        await Task.Delay(3000);

        var getResponseAuthExpired = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        
        Assert.Equal(HttpStatusCode.Unauthorized, getResponseAuthExpired.StatusCode);
        
        // Refresh JWT
        var refreshResponse = await _client.PostAsJsonAsync("/api/v1/account/RenewRefreshToken", new RefreshTokenDto()
        {
            Jwt = responseData.Jwt,
            RefreshToken = responseData.RefreshToken
        });
        
        var refreshedResponseData = await refreshResponse.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(refreshedResponseData);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshedResponseData.Jwt);
        
        var getResponse2 = await _client.GetAsync("/api/v1/persons/getProfileInfo");
        getResponse2.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task UserWithNoRoles_CannotAccess_ManagerController()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "bob@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.Jwt);
        
        var getResponse = await _client.GetAsync("/api/v1/warehouses/getWarehouses");
        Assert.Equal(HttpStatusCode.Forbidden, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task UserWithManagerRole_CanAccess_ManagerController()
    {
        // Arrange
        var loginData = new LoginDto()
        {
            Email = "manager@test.ee",
            Password = "Abc123-"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/account/login", loginData);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<JwtResponseDto>();
        Assert.NotNull(responseData);
        Assert.True(responseData.Jwt.Length > 128);
        Assert.True(responseData.RefreshToken.Length == Guid.NewGuid().ToString().Length);
        
        
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData.Jwt);
        
        var getResponse = await _client.GetAsync("/api/v1/warehouses/getWarehouses");
        getResponse.EnsureSuccessStatusCode();
        
    }
}