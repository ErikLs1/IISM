using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseRepository<OrderDalDto, Order>, IOrderRepository
{
    public OrderRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new OrderUowMapper())
    {
    }

    public async Task<List<OrderDalDto>> GetOrdersByPersonIdAsync(Guid personId)
    {
        var res = await GetQuery()
            .Where(o => o.PersonId == personId)
            .Include(o => o.OrderProducts)!
            .ThenInclude(op => op.Product)
            .Select(x => Mapper.Map(x)!)
            .ToListAsync();

        return res;
    }

    public async Task<List<OrderDalDto>> GetAllPlacedOrdersAsync()
    {
        return await GetQuery()
            .Include(o => o.OrderProducts)!
            .ThenInclude(op => op.Product)
            .Include(o => o.Person)
            .Select(x => Mapper.Map(x)!)
            .ToListAsync();
    }

    public async Task UpdateOrderStatus(Guid orderId, string orderStatus)
    { 
        var res =  await GetQuery()                       // IQueryable<Order>
            .FirstOrDefaultAsync(o => o.Id == orderId);
        res!.OrderStatus = orderStatus;
    }
}