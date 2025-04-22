using Base.DAL.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUow<TDbContext> : IBaseUow
    where TDbContext : DbContext
{
    protected readonly TDbContext _uowDbContext;

    public BaseUow(TDbContext context)
    {
        _uowDbContext = context;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _uowDbContext.SaveChangesAsync();
    }
}