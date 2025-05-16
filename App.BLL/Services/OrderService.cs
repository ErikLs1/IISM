using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.DAL.DTO;
using App.DTO.V1.DTO;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseService<OrderBllDto, OrderDalDto, IOrderRepository>, IOrderService
{
    private readonly IAppUow _uow;
    public OrderService(
        IAppUow uow, 
        IMapper<OrderBllDto, OrderDalDto> mapper) : base(uow, uow.OrderRepository, mapper)
    {
        _uow = uow;
    }

    public async Task<OrderBllDto> PlaceOrderAsync(Guid personId, CreateOrderDto dto)
    {
        // TODO - MAPPING (LATER)
        var order = new OrderDalDto()
        {
            Id = Guid.NewGuid(),
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

    // TODO MAPPER LATER
    public async Task<IEnumerable<UserOrdersDto>> GetUsersOrdersAsync(Guid personId)
    {
        var orders = await _uow.OrderRepository.GetOrdersByPersonIdAsync(personId);
        
        return orders.Select(o => new UserOrdersDto
        {
            OrderTotalPrice = o.OrderTotalPrice,
            OrderShippingAddress = o.OrderShippingAddress,
            OrderStatus = o.OrderStatus,
            Products = o.OrderProducts!.Select(op => new OrderProductDto
            {
                Quantity = op.Quantity,
                OrderProductPrice = op.TotalPrice,
                ProductName = op.Product!.ProductName,
                ProductDescription = op.Product!.ProductDescription
            }).ToList()
        });
    }

    // TODO MAPPER LATER
    public async Task<IEnumerable<PlacedOrderDto>> GetAllPlacedOrdersAsync()
    {
        var orders = await _uow.OrderRepository.GetAllPlacedOrdersAsync();
        
        return orders.Select(o => new PlacedOrderDto()
        {
            OrderId = o.Id,
            CustomerFirstName = o.Person!.PersonFirstName,
            CustomerLastName = o.Person!.PersonLastName,
            TotalNumberOfProducts = o.OrderProducts!.Sum(op => op.Quantity),
            OrderedAt = o.CreatedAt,
            OrderStatus = o.OrderStatus,
            Products = o.OrderProducts!.Select(op => new OrderProductDto
            {
                Quantity = op.Quantity,
                OrderProductPrice = op.TotalPrice,
                ProductName = op.Product!.ProductName,
                ProductDescription = op.Product!.ProductDescription
            }).ToList()
        });
    }

    
    public async Task ChangeOrderStatusAsync(Guid orderId, string newStatus)
    {
        await _uow.OrderRepository.UpdateOrderStatus(orderId, newStatus);
        await _uow.SaveChangesAsync();
    }
}