using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Contracts;

namespace App.BLL.Mappers;

public class InventoryBllMapper : IBllMapper<InventoryBllDto, InventoryDalDto>
{
    public InventoryDalDto? Map(InventoryBllDto? entity)
    {
        throw new NotImplementedException();
    }

    public InventoryBllDto? Map(InventoryDalDto? entity)
    {
        throw new NotImplementedException();
    }
}