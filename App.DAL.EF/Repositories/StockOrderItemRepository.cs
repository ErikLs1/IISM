using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockOrderItemRepository : BaseRepository<StockOrderItemDto, StockOrderItem>, IStockOrderItemRepository
{
    public StockOrderItemRepository(AppDbContext repositoryDbContext, IMapper<StockOrderItemDto, StockOrderItem> mapper) : base(repositoryDbContext, mapper)
    {
    }
}