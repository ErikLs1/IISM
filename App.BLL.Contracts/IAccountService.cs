using App.BLL.DTO;
using App.BLL.DTO.Identity;
using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAccountService 
{
    Task<JwtResponseBllDto> RegisterAsync(
        RegisterBllDto dto,
        int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds);
    
    Task<JwtResponseBllDto> LoginAsync(
        LoginBllDto dto,
        int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds);
    
    Task<JwtResponseBllDto> RenewTokenAsync(
        RefreshTokenBllDto dto,
        int? jwtExpiresInSeconds,
        int? refreshTokenExpiresInSeconds);
    Task Logout(Guid userId, string refreshToken);
}