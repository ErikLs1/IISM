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
    [ProducesResponseType(typeof(IEnumerable<WarehouseInventoryItemsDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<WarehouseInventoryItemsDto>>> GetProductsForWarehouse([FromQuery] Guid warehouseId)
    {
        var data = await _bll.InventoryService.GetProductsByWarehouseIdAsync(warehouseId);
        var res = data
            .Select(i => new WarehouseInventoryItemsDto()
            {
                ProductId = i.ProductId,
                ProductName = i.Product!.ProductName,
                ProductDescription = i.Product!.ProductDescription,
                Quantity = i.Quantity
            })
            .ToList();
        return res;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<InventoryProductsDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<InventoryProductsDto>>> GetInventoryProducts()
    {
        var data = await _bll.InventoryService.GetAllInventoryProductsAsync();
        var res = data
            .Select(p => new InventoryProductsDto()
            {
                WarehouseId = p.WarehouseId,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName,
                ProductPrice = Math.Round(p.ProductPrice * 1.5m, 2),
                WarehouseCity = p.WarehouseCity,
                WarehouseState = p.WarehouseState,
                WarehouseCountry = p.WarehouseCountry,
                ProductDescription = p.ProductDescription
            })
            .ToList();
        return res;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<InventoryProductsDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<InventoryProductsDto>>> GetFilteredInventoryProducts(
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? category,
        [FromQuery] string? productName
        )
    {
        var data = await _bll.InventoryService.GetFilteredInventoryProductsAsync(
            minPrice, maxPrice, category, productName);
        var res = data
            .Select(p => new InventoryProductsDto()
            {
                WarehouseId = p.WarehouseId,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryName = p.CategoryName,
                ProductPrice = p.ProductPrice,
                WarehouseCity = p.WarehouseCity,
                WarehouseState = p.WarehouseState,
                WarehouseCountry = p.WarehouseCountry,
                ProductDescription = p.ProductDescription
            })
            .ToList();
        return res;
    }
}