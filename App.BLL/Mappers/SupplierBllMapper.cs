using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class SupplierBllMapper : IBllMapper<SupplierBllDto, SupplierDalDto>
{
    public SupplierDalDto? Map(SupplierBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public SupplierBllDto? Map(SupplierDalDto? entity)
    {
        throw new NotImplementedException();
    }
}