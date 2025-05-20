using App.BLL.Contracts;
using App.DTO.V1.DTO;
using App.DTO.V1.Mappers;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class InventoriesController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly InventoryProductsMapper _inventoryProductsMapper = new InventoryProductsMapper();
    private readonly WarehouseInventoryItemsMapper _warehouseItemsMapper = new WarehouseInventoryItemsMapper();

    /// <inheritdoc />
    public InventoriesController(IAppBll bll)
    {
        _bll = bll;
    }
    
    // TODO - Pagination later
    /// <summary>
    /// 
    /// </summary>
    /// <param name="warehouseId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "manager")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseInventoryItemsDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<WarehouseInventoryItemsDto>>> GetProductsForWarehouse([FromQuery] Guid warehouseId)
    {
        var data = await _bll.InventoryService.GetProductsByWarehouseIdAsync(warehouseId);
        var res = data
            .Select(i => _warehouseItemsMapper.Map(i)!)
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
            .Select(p => _inventoryProductsMapper.Map(p)!)
            .ToList();
        return res;
    }
    
    // TODO - Pagination later
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<InventoryProductsDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<PagedData<InventoryProductsDto>>> GetFilteredInventoryProducts(
        [FromQuery] int pageIndex,
        [FromQuery] int pageSize,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? category,
        [FromQuery] string? productName
        )
    {
        var data = await _bll.InventoryService.GetPagedDataAsync(
            pageIndex, pageSize, minPrice, maxPrice, category, productName);
        var res = data.Items
            .Select(p => _inventoryProductsMapper.Map(p)!)
            .ToList();
        return new PagedData<InventoryProductsDto>
        {
            Items = res,
            TotalCount = data.TotalCount,
            PageIndex = data.PageIndex,
            PageSize = data.PageSize
        };
    }
    
    // TODO CONTROLLER FOR FILTERING ORDERS
}