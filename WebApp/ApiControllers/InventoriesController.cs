using App.BLL.Contracts;
using App.DTO.V1.DTO;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class InventoriesController : ControllerBase
{
    private readonly IAppBll _bll;

    /// <inheritdoc />
    public InventoriesController(IAppBll bll)
    {
        _bll = bll;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="warehouseId"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<InventoryItemDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetProductsForWarehouse([FromQuery] Guid warehouseId)
    {
        var data = await _bll.InventoryService.GetProductsByWarehouseIdAsync(warehouseId);
        var res = data
            .Select(i => new InventoryItemDto()
            {
                ProductId = i.ProductId,
                ProductName = i.Product!.ProductName,
                ProductDescription = i.Product!.ProductDescription,
                Quantity = i.Quantity
            })
            .ToList();
        return res;
    }
}