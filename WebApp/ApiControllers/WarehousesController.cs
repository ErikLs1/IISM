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
public class WarehousesController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly WarehouseMapper _mapper = new WarehouseMapper();
    private readonly WarehouseFiltersMapper _filtersMapper = new WarehouseFiltersMapper();
    
    /// <inheritdoc />
    public WarehousesController(IAppBll bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Retrieves all existing warehouses.
    /// </summary>
    /// <returns>
    /// 200 OK with list of warehouses that exists.
    /// 404 Not Found If no warehouses was found.
    /// </returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetWarehouses()
    {
        var data = await _bll.WarehouseService.AllAsync();
        var res = data.Select(x => _mapper.Map(x)!).ToList();
        return res;
    }

    /// <summary>
    /// Retrieves a single warehouse by its id.
    /// </summary>
    /// <param name="id">The unique identifier of the warehouse to retrieve.</param>
    /// <returns>
    /// 200 OK with WarehouseDto when data is found.
    /// 404 Not Found if warehouse with provided id does not exist.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseDto>> GetWarehouse(Guid id)
    {
        var warehouse = await _bll.WarehouseService.FindAsync(id);

        if (warehouse == null)
        {
            return NotFound();
        }

        return _mapper.Map(warehouse)!;
    }

    /// <summary>
    /// Update an existing warehouse.
    /// </summary>
    /// <param name="id">The id of the warehouse to update.</param>
    /// <param name="warehouse">The updated warehouse data.</param>
    /// <returns>
    /// 204 No Content when update succeeds;
    /// 400 Bad Request If provided id does not match;
    /// 404 Not Found If warehouse does not exist;
    /// </returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWarehouse(Guid id, WarehouseDto warehouse)
    {
        if (id != warehouse.Id)
        {
            return BadRequest();
        }

        await _bll.WarehouseService.UpdateAsync(_mapper.Map(warehouse)!);
        await _bll.SaveChangesAsync();
        
        return NoContent();
    }
    
    /// <summary>
    /// Creates a new warehouse
    /// </summary>
    /// <param name="warehouse">The data of the warehouse to create.</param>
    /// <returns>
    /// 201 Created if warehouse was successfully created;
    /// 400 Bad Request If inputs are invalid;
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WarehouseDto>> CreateWarehouse(WarehouseCreateDto warehouse)
    {
        var bllEntity = _mapper.Map(warehouse);
        _bll.WarehouseService.Add(bllEntity!);
        await _bll.SaveChangesAsync();
        
        return CreatedAtAction("GetWarehouse", new
        {
            id = bllEntity?.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, warehouse);
    }

    /// <summary>
    /// Deletes a warehouse by id.
    /// </summary>
    /// <param name="id">The unique identifier of the warehouse to delete.</param>
    /// <returns>
    /// 204 No Content When deletion is successful;
    /// 404 Not Found If warehouse was not found;
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWarehouse(Guid id)
    {
        await _bll.WarehouseService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Warehouse filtered by the address.
    /// </summary>
    /// <param name="street">(Optional) street name to filter by.</param>
    /// <param name="city">(Optional) city name to filter by.</param>
    /// <param name="state">(Optional) state name to filter by.</param>
    /// <param name="country">(Optional) country name to filter by.</param>
    /// <returns>
    /// 200 OK  and list of warehouses matching the filters.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetFilteredWarehouses(
        [FromQuery] string? street,
        [FromQuery] string? city,
        [FromQuery] string? state,
        [FromQuery] string? country
    )
    {
        var warehouses = await _bll
            .WarehouseService
            .GetFilteredWarehousesAsync(street, city, state, country);
        var res = warehouses.Select(x => _mapper.Map(x)!);
        return Ok(res);
    }
    
    /// <summary>
    /// Retrieves the distinct filter values for warehouse.
    /// </summary>
    /// <returns>
    /// 200 OK Containing lists of streets, cities, states and countries.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(WarehouseFiltersDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<WarehouseFiltersDto>> GetFilters()
    {
        var filters = await _bll.WarehouseService.GetWarehouseFiltersAsync();
        var dto = _filtersMapper.Map(filters);
        return Ok(dto);
    }
}