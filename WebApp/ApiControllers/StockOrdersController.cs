using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.DTO.V1.DTO;
using App.DTO.V1.Mappers;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "manager")]
public class StockOrdersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly StockOrderMapper _mapper = new StockOrderMapper();

    /// <inheritdoc />
    public StockOrdersController(IAppBll bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Places a new stock order.
    /// </summary>
    /// <param name="dto">The details of the order.</param>
    /// <returns>
    /// 201 Created meaning that the order was created and stock added to warehouse.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> PlaceStockOrder(CreateStockOrderDto dto)
    {
        
        var result = await _bll.StockOrderService.PlaceStockOrderAsync(dto);
        var stockOrder = _mapper.Map(result);
        return CreatedAtAction("",new
        {
            id = stockOrder!.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, stockOrder);
    }
}