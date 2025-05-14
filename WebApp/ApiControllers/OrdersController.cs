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

    // TODO - ORDER PLACING
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), 201)]
    public async Task<IActionResult> PlaceTheOrder(CreateOrderDto dto)
    {
        // Get user id
        var userId = User.GetUserId();
        
        // Get currently logged in personId
        var personId = await _bll.PersonService.GetPersonIdByUserIdAsync(userId);
        
        // 3) log it
        _logger.LogInformation("=========================== ======== PersonId={PersonId}", personId);

        // Assign person id to dto
        var bllDto = await _bll.OrderService.PlaceOrderAsync(personId, dto);

        return CreatedAtAction("", new
        {
            id = bllDto.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        });
    }
    
    /*// TODO - ENDPOINT FOR USERS ORDERS
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    [ProducesResponseType(typeof(OrderDto), 200)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetUsersOrders()
    {
        throw new NotImplementedException();
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
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrder()
    {
        throw new NotImplementedException();
    }*/
}