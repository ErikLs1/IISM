using App.BLL.Contracts;
using App.BLL.DTO;
using App.BLL.DTO.Identity;
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

    public async Task<ProfileInfoBllDto> GetProfileAsync(Guid userId)
    {
        var dal = await ServiceRepository.FindByUserIdAsync(userId);
        var res = new ProfileInfoBllDto()
        {
            PersonFirstName = dal.PersonFirstName,
            PersonLastName = dal.PersonLastName,
            PersonPhoneNumber = dal.PersonPhoneNumber,
            PersonAddress = dal.PersonAddress,
            PersonGender = dal.PersonGender,
            PersonBirthDate = dal.PersonDateOfBirth
        };
        return res;
    }
}