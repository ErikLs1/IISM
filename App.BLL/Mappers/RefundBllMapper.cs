using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class RefundBllMapper : IBllMapper<RefundBllDto, RefundDalDto>
{
    public RefundDalDto? Map(RefundBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public RefundBllDto? Map(RefundDalDto? entity)
    {
        throw new NotImplementedException();
    }
}