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
public class ProductSuppliersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly ProductSuppliersMapper _mapper = new ProductSuppliersMapper();

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
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductSupplierDto>), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<ProductSupplierDto>>> GetProductSuppliers()
    {
        var data = await _bll.ProductSupplierService.GetAllProductSuppliersAsync();
        var res = data.Select(x => _mapper.Map(x)!).ToList();
        return res;
    }

    /*// GET: api/ProductSuppliers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductSupplier>> GetProductSupplier(Guid id)
    {
        var productSupplier = await _context.ProductSuppliers.FindAsync(id);

        if (productSupplier == null)
        {
            return NotFound();
        }

        return productSupplier;
    }

    // PUT: api/ProductSuppliers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductSupplier(Guid id, ProductSupplier productSupplier)
    {
        if (id != productSupplier.Id)
        {
            return BadRequest();
        }

        _context.Entry(productSupplier).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductSupplierExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/ProductSuppliers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductSupplier>> PostProductSupplier(ProductSupplier productSupplier)
    {
        _context.ProductSuppliers.Add(productSupplier);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductSupplier", new { id = productSupplier.Id }, productSupplier);
    }

    // DELETE: api/ProductSuppliers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductSupplier(Guid id)
    {
        var productSupplier = await _context.ProductSuppliers.FindAsync(id);
        if (productSupplier == null)
        {
            return NotFound();
        }

        _context.ProductSuppliers.Remove(productSupplier);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductSupplierExists(Guid id)
    {
        return _context.ProductSuppliers.Any(e => e.Id == id);
    }*/
}