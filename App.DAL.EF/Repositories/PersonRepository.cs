using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<Person>> AllAsync(Guid userId = default)
    {
        var query = GetQuery(userId);
        query = query.Include(p => p.User);
        return await query.ToListAsync();
    }

    public async Task<int> GetPersonCountByNameAsync(string name, Guid userId)
    {
        var query = GetQuery(userId);
        return await query
            .CountAsync(p => p.PersonFirstName == name);
    }
}