using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<int> GetPersonCountByNameAsync(string name, Guid userId);
}