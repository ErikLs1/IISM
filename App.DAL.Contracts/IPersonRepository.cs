using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IPersonRepository : IBaseRepository<PersonDalDto>, IPersonRepositoryCustom
{
}

public interface IPersonRepositoryCustom
{
    Task<int> GetPersonCountByNameAsync(string name, Guid userId);
}