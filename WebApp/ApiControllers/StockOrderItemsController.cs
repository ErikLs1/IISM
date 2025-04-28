using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class StockOrderItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockOrderItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/StockOrderItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockOrderItem>>> GetStockOrderItems()
    {
        return await _context.StockOrderItems.ToListAsync();
    }

    // GET: api/StockOrderItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StockOrderItem>> GetStockOrderItem(Guid id)
    {
        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);

        if (stockOrderItem == null)
        {
            return NotFound();
        }

        return stockOrderItem;
    }

    // PUT: api/StockOrderItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStockOrderItem(Guid id, StockOrderItem stockOrderItem)
    {
        if (id != stockOrderItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(stockOrderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StockOrderItemExists(id))
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

    // POST: api/StockOrderItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<StockOrderItem>> PostStockOrderItem(StockOrderItem stockOrderItem)
    {
        _context.StockOrderItems.Add(stockOrderItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStockOrderItem", new { id = stockOrderItem.Id }, stockOrderItem);
    }

    // DELETE: api/StockOrderItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockOrderItem(Guid id)
    {
        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);
        if (stockOrderItem == null)
        {
            return NotFound();
        }

        _context.StockOrderItems.Remove(stockOrderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StockOrderItemExists(Guid id)
    {
        return _context.StockOrderItems.Any(e => e.Id == id);
    }
}