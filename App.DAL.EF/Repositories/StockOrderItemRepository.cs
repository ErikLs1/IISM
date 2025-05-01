using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockOrderItemRepository : BaseRepository<StockOrderItemDalDto, StockOrderItem>, IStockOrderItemRepository
{
    public StockOrderItemRepository(AppDbContext repositoryDbContext, IUowMapper<StockOrderItemDalDto, StockOrderItem> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}