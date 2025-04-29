using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class InventoryRepository : BaseRepository<InventoryDto, Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new InventoryMapper())
    {
    }
}