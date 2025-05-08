using System.Security.Authentication;
using System.Security.Claims;
using App.BLL.Contracts;
using App.BLL.DTO.Identity;
using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace App.BLL.Services;

public class AccountService : IAccountService
{
    private readonly IAppUow _uow;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;

    private const string SettingsJwtPrefix = "JWTSecurity";
    private const string SettingsJwtKey = SettingsJwtPrefix + ":Key";
    private const string SettingsJwtIssuer = SettingsJwtPrefix + ":Issuer";
    private const string SettingsJwtAudience = SettingsJwtPrefix + ":Audience";
    private const string SettingsJwtExpiresInSeconds = SettingsJwtPrefix + ":ExpiresInSeconds";
    private const string SettingsJwtRefreshTokenExpiresInSeconds = SettingsJwtPrefix + ":RefreshTokenExpiresInSeconds";



    public AccountService(
        IAppUow uow, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, IConfiguration configuration)
    {
        _uow = uow;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public Task<JwtResponseBllDto> RegisterAsync(RegisterBllDto dto, int? jwtExpiresInSeconds, int? refreshTokenExpiresInSeconds)
    {
        throw new NotImplementedException();
    }

    public async Task<JwtResponseBllDto> LoginAsync(LoginBllDto dto, int? jwtExpiresInSeconds, int? refreshTokenExpiresInSeconds)
    {
        // verify user
        var user = await _userManager.FindByEmailAsync(dto.Email) ??
                   throw new InvalidCredentialException("User with email " + dto.Email + " does not exist.");

        if (!(await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false)).Succeeded)
            throw new InvalidCredentialException("Wrong credentials.");

        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.FirstName));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        await _uow.RefreshTokenRepository.DeleteExpiredTokenAsync(user.Id);

        var newRefreshToken = new RefreshTokenDalDto()
        {
            UserId = user.Id,
            RefreshToken = Guid.NewGuid().ToString(),
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, SettingsJwtRefreshTokenExpiresInSeconds)
        };
        
        _uow.RefreshTokenRepository.Add(newRefreshToken);
        await _uow.SaveChangesAsync();
        
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration[SettingsJwtKey]!,
            _configuration[SettingsJwtIssuer]!,
            _configuration[SettingsJwtAudience]!,
            GetExpirationDateTime(jwtExpiresInSeconds, SettingsJwtExpiresInSeconds)
        );

        var role = await _userManager.GetRolesAsync(user);
        var responseData = new JwtResponseBllDto()
        {
            Jwt = jwt,
            RefreshToken = newRefreshToken.RefreshToken,
            Role = role.FirstOrDefault()!
        };

        return responseData;
    }

    public Task<JwtResponseBllDto> RenewTokenAsync(RefreshTokenBllDto dto, int? jwtExpiresInSeconds, int? refreshTokenExpiresInSeconds)
    {
        throw new NotImplementedException();
    }

    public Task Logout(Guid userId, string refreshToken)
    {
        throw new NotImplementedException();
    }
    
    private Task<JwtResponseBllDto> CreateJwtTokenAsync(AppUser user, int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds, AppRefreshToken? refreshToken)
    {
        throw new NotImplementedException();
    }
    
    private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds <  int.Parse(_configuration[settingsKey]!)
            ? expiresInSeconds
            : int.Parse(_configuration[settingsKey]!);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
}