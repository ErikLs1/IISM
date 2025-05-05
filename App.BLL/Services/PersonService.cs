using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<PersonBllDto, PersonDalDto, IPersonRepository>, IPersonService
{
    public PersonService(
        IAppUow serviceUow, 
        IMapper<PersonBllDto, PersonDalDto> mapper) : base(serviceUow, serviceUow.PersonRepository, mapper)
    {
    }

    public virtual async Task<int> GetPersonCountByNameAsync(string name, Guid userId)
    {
        var count = await ServiceRepository.GetPersonCountByNameAsync(name, userId);
        return count;
    }
}