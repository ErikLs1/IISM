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
public class CategoriesController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly CategoryMapper _mapper = new CategoryMapper();
    private readonly CategoryNamesMapper _categoryNamesMapper = new CategoryNamesMapper();

    /// <inheritdoc />
    public CategoriesController(IAppBll bll)
    {
        _bll = bll;
    }

    
    /// <summary>
    /// Get all categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(404)] 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var data = await _bll.CategoryService.AllAsync();
        var res = data.Select(x => _mapper.Map(x)!).ToList();
        return res;
    }

    /// <summary>
    /// Get category by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(404)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
        var category = await _bll.CategoryService.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return _mapper.Map(category)!;
    }

    /// <summary>
    /// Update category.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryDto category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        await _bll.CategoryService.UpdateAsync(_mapper.Map(category)!);
        await _bll.SaveChangesAsync();
        
        return NoContent();
    }

    /// <summary>
    /// Create new category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryCreateDto category)
    {
        var bllEntity = _mapper.Map(category);
        _bll.CategoryService.Add(bllEntity!);
        await _bll.SaveChangesAsync();
        
        return CreatedAtAction("GetWarehouse", new
        {
            id = bllEntity?.Id,
            version = HttpContext.GetRequestedApiVersion()!.ToString()
        }, category);}

    /// <summary>
    /// Delete category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _bll.CategoryService.RemoveAsync(id);
        await _bll.SaveChangesAsync();
        return NoContent();
    }
    
    /// <summary>
    /// Gets all distinct product category names
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(CategoryNamesDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryNamesDto>> GetDistinctCategoryNames()
    {
        var filters = await _bll.CategoryService.GetDistinctCategoryNamesAsync();
        var dto = _categoryNamesMapper.Map(filters);
        return Ok(dto);
    }
}