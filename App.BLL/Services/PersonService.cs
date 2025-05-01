using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class PersonService : BaseService<PersonBllDto, PersonDalDto>, IPersonService
{
    public PersonService(
        IBaseUow serviceUow, 
        IBaseRepository<PersonDalDto, Guid> serviceRepository, 
        IBllMapper<PersonBllDto, PersonDalDto, Guid> bllMapper) : base(serviceUow, serviceRepository, bllMapper)
    {
    }
}