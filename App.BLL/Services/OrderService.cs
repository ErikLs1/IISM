using App.BLL.Contracts;
using App.BLL.DTO;
using App.BLL.Mappers;
using App.DAL.Contracts;
using App.DAL.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseService<OrderBllDto, OrderDalDto, IOrderRepository>, IOrderService
{
    private readonly IAppUow _uow;
    private readonly PlacedOrderBllMapper _placedOrderMapper = new PlacedOrderBllMapper();
    private readonly UserOrdersBllMapper _userOrdersMapper = new UserOrdersBllMapper();
    
    public OrderService(
        IAppUow uow, 
        IMapper<OrderBllDto, OrderDalDto> mapper) : base(uow, uow.OrderRepository, mapper)
    {
        _uow = uow;
    }

    // TODO - REFACTORING (LATER)
    public async Task<OrderBllDto> PlaceOrderAsync(Guid personId, CreateOrderBllDto dto)
    {
        var order = new OrderDalDto()
        {
            PersonId = personId,
            OrderShippingAddress = dto.ShippingAddress,
            OrderStatus = "PENDING", // TODO ENUM (LATER)
            OrderTotalPrice = dto.Products
                .Sum(i => _uow.ProductRepository
                    .GetProductPriceById(i.ProductId).Result * i.Quantity)
        };
        
        _uow.OrderRepository.Add(order);

        foreach (var product in dto.Products)
        {
            var prod = await _uow.ProductRepository.GetProductPriceById(product.ProductId);
            var item = new OrderProductDalDto() // TODO - MAPPING (LATER)
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                TotalPrice = prod * product.Quantity
            };
            _uow.OrderProductRepository.Add(item);
        }

        var payment = new PaymentDalDto()
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            PaymentMethod = dto.PaymentMethod,
            PaymentStatus = "COMPLETED",
            PaymentAmount = order.OrderTotalPrice,
            PaymentDate = DateTime.UtcNow
        };
        
        _uow.PaymentRepository.Add(payment);

        await _uow.SaveChangesAsync();
        var created = await _uow.OrderRepository.FindAsync(order.Id);

        return Mapper.Map(created)!;
    }

    public async Task<IEnumerable<UserOrdersBllDto>> GetUsersOrdersAsync(Guid personId)
    {
        var orders = await _uow.OrderRepository.GetOrdersByPersonIdAsync(personId);
        return orders.Select(o => _userOrdersMapper.Map(o)!).ToList();
    }

    public async Task<IEnumerable<PlacedOrderBllDto>> GetAllPlacedOrdersAsync()
    {
        var orders = await _uow.OrderRepository.GetAllPlacedOrdersAsync();
        return orders.Select(o => _placedOrderMapper.Map(o)!).ToList();
    }

    
    public async Task ChangeOrderStatusAsync(Guid orderId, string newStatus)
    {
        await _uow.OrderRepository.UpdateOrderStatus(orderId, newStatus);
        await _uow.SaveChangesAsync();
    }
}