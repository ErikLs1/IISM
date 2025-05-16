using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DTO.V1.DTO;
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
    private readonly ILogger<OrdersController> _logger;

    /// <inheritdoc />
    public OrdersController(IAppBll bll, ILogger<OrdersController> logger)
    {
        _bll = bll;
        _logger = logger;
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
        
        var bllDto = await _bll.OrderService.PlaceOrderAsync(personId, dto);

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
        return Ok(userOrders);
    }
    
    // TODO - ENDPOINT FOR ALL ORDER (FOR MANAGER)
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
        return Ok(orders);
    }
    
    /// <summary>
    /// Manager-only: change the status of an existing order.
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "manager")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ChangeOrderStatus(
        [FromBody] ChangeOrderStatusDto dto)
    {
        try
        {
            await _bll.OrderService.ChangeOrderStatusAsync(dto.OrderId, dto.OrderStatus);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}