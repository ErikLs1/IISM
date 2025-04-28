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
public class StockOrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockOrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/StockOrders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StockOrder>>> GetStockOrders()
    {
        return await _context.StockOrders.ToListAsync();
    }

    // GET: api/StockOrders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StockOrder>> GetStockOrder(Guid id)
    {
        var stockOrder = await _context.StockOrders.FindAsync(id);

        if (stockOrder == null)
        {
            return NotFound();
        }

        return stockOrder;
    }

    // PUT: api/StockOrders/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStockOrder(Guid id, StockOrder stockOrder)
    {
        if (id != stockOrder.Id)
        {
            return BadRequest();
        }

        _context.Entry(stockOrder).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StockOrderExists(id))
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

    // POST: api/StockOrders
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<StockOrder>> PostStockOrder(StockOrder stockOrder)
    {
        _context.StockOrders.Add(stockOrder);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStockOrder", new { id = stockOrder.Id }, stockOrder);
    }

    // DELETE: api/StockOrders/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStockOrder(Guid id)
    {
        var stockOrder = await _context.StockOrders.FindAsync(id);
        if (stockOrder == null)
        {
            return NotFound();
        }

        _context.StockOrders.Remove(stockOrder);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StockOrderExists(Guid id)
    {
        return _context.StockOrders.Any(e => e.Id == id);
    }
}