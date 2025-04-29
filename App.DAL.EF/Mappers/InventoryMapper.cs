using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.EF.Mappers;

public class InventoryMapper : IMapper<InventoryDto, Inventory>
{
    public InventoryDto? Map(Inventory? entity)
    {
        throw new NotImplementedException();
    }

    public Inventory? Map(InventoryDto? entity)
    {
        throw new NotImplementedException();
    }
}