using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class WarehouseMapper : IMapper<WarehouseDto, Warehouse>
{
    public WarehouseDto? Map(Warehouse? entity)
    {
        throw new NotImplementedException();
    }

    public Warehouse? Map(WarehouseDto? entity)
    {
        throw new NotImplementedException();
    }
}