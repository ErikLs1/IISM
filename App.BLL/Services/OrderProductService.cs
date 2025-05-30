using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Contracts;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class OrderProductService : BaseService<OrderProductBllDto, OrderProductDalDto, IOrderProductRepository>, IOrderProductService
{
    public OrderProductService(
        IAppUow serviceUow, 
        IMapper<OrderProductBllDto, OrderProductDalDto> mapper) : base(serviceUow, serviceUow.OrderProductRepository, mapper)
    {
    }

    public async override Task<IEnumerable<OrderProductBllDto>> AllAsync(Guid userId = default)
    {
        var entities = await ServiceRepository.AllAsync(userId);
        return entities.Select(e => Mapper.Map(e)!).ToList();
    }
}