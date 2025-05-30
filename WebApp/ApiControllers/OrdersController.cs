using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DTO.V1.DTO;
using App.DTO.V1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly CreateOrderMapper _createOrderMapper = new CreateOrderMapper();
    private readonly UserOrdersMapper _userOrdersMapper = new UserOrdersMapper();
    private readonly PlacedOrdersMapper _placedOrdersMapper = new PlacedOrdersMapper();

    /// <inheritdoc />
    public OrdersController(IAppBll bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), 201)]
    public async Task<IActionResult> PlaceTheOrder(CreateOrderDto dto)
    {
        var userId = User.GetUserId();
        var personId = await _bll.PersonService.GetPersonIdByUserIdAsync(userId);
        var bllDto = await _bll.OrderService.PlaceOrderAsync(personId, _createOrderMapper.Map(dto)!);
        return CreatedAtAction("", new
        {
            id = bllDto.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [ProducesResponseType(typeof(UserOrdersDto), 200)]
    public async Task<ActionResult<IEnumerable<UserOrdersDto>>> GetUsersOrders()
    {
        var userId = User.GetUserId();
        var personId = await _bll.PersonService.GetPersonIdByUserIdAsync(userId);
        var userOrders = await _bll.OrderService.GetUsersOrdersAsync(personId);
        var mapped = userOrders.Select(x => _userOrdersMapper.Map(x));
        return Ok(mapped);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [Authorize(Roles = "manager")]
    [ProducesResponseType(typeof(OrderDto), 200)]
    public async Task<ActionResult<IEnumerable<PlacedOrderDto>>> GetAllPlacedOrder()
    {
        var orders = await _bll.OrderService.GetAllPlacedOrdersAsync();
        var mapped =  orders.Select(x => _placedOrdersMapper.Map(x));
        return Ok(mapped);
    }
    
    /// <summary>
    /// 
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "manager")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ChangeOrderStatus(
        [FromBody] ChangeOrderStatusDto dto)
    {
            await _bll.OrderService.ChangeOrderStatusAsync(dto.OrderId, dto.OrderStatus);
            return NoContent();
    }
}