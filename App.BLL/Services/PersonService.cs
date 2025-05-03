using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<PersonBllDto, PersonDalDto, IPersonRepository>, IPersonService
{
    public PersonService(
        IAppUow serviceUow, 
        IBllMapper<PersonBllDto, PersonDalDto> bllMapper) : base(serviceUow, serviceUow.PersonRepository, bllMapper)
    {
    }

    public virtual Task<int> GetPersonCountByNameAsync(string name, Guid userId)
    {
        throw new NotImplementedException();
    }
}