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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WarehousesController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly WarehouseMapper _mapper = new WarehouseMapper();
    
    /// <inheritdoc />
    public WarehousesController(IAppBll bll)
    {
        _bll = bll;
    }

    /// <summary>
    /// Get all existing warehouses.
    /// </summary>
    /// <returns>List of warehouses.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<WarehouseDto>>> GetWarehouses()
    {
        var data = await _bll.WarehouseService.AllAsync();
        var res = data.Select(x => _mapper.Map(x)!).ToList();
        return res;
    }

    /// <summary>
    /// Get the warehouse data by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), 200)]
    [ProducesResponseType(404)]
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
    /// Update warehouse.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="warehouse"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), 201)]
    [ProducesResponseType(404)]
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
    /// Create new warehouse
    /// </summary>
    /// <param name="warehouse"></param>
    /// <returns></returns>
    [HttpPost]
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
    /// Delete warehouse by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse(Guid id)
    {
        await _bll.WarehouseService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="street"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="country"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WarehouseDto>), 200)]
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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(WarehouseFiltersDto), 200)]
    public async Task<ActionResult<WarehouseFiltersDto>> GetFilters()
    {
        var filters = await _bll.WarehouseService.GetWarehouseFiltersAsync();
        var dto = new WarehouseFiltersDto()
        {
            States = filters.States,
            Streets = filters.Streets,
            Cities = filters.Cities,
            Countries = filters.Countries
        };

        return Ok(dto);
    }
}