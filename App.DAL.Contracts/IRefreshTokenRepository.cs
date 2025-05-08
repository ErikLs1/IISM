using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IRefreshTokenRepository :  IBaseRepository<RefreshTokenDalDto>
{
    Task<int> DeleteExpiredTokenAsync(Guid userId);
}