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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "manager")]
public class ProductSuppliersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly ProductSuppliersMapper _mapper = new ProductSuppliersMapper();
    private readonly ProductSupplierFiltersMapper _filtersMapper = new ProductSupplierFiltersMapper();

    /// <inheritdoc />
    public ProductSuppliersController(IAppBll bll)
    {
        _bll = bll;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ProductSupplierFiltersDto), 200)]
    public async Task<ActionResult<ProductSupplierFiltersDto>> GetFilters()
    {
        var filters = await _bll.ProductSupplierService.GetProductSupplierFilterAsync();
        var dto = _filtersMapper.Map(filters);
        return Ok(dto);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="country"></param>
    /// <param name="category"></param>
    /// <param name="supplier"></param>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PagedData<ProductSupplierDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<PagedData<ProductSupplierDto>>> GetFilteredProductSuppliers(
        [FromQuery] int pageIndex,
        [FromQuery] int pageSize,
        [FromQuery] string? city,
        [FromQuery] string? state,
        [FromQuery] string? country,
        [FromQuery] string? category,
        [FromQuery] string? supplier
        )
    {
        var data = await _bll.ProductSupplierService.GetPagedDataAsync(
            pageIndex, pageSize, city, state, country, category, supplier);
        var dto = data.Items.Select(x => _mapper.Map(x)!);
        var res = new PagedData<ProductSupplierDto>
        {
            Items = dto,
            TotalCount = data.TotalCount,
            PageIndex = data.PageIndex,
            PageSize = data.PageSize
        };
        return Ok(res);
    }
}