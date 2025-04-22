using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class StockOrderItemsController : Controller
{
    private readonly AppDbContext _context;

    public StockOrderItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: StockOrderItems
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.StockOrderItems.Include(s => s.Product).Include(s => s.StockOrder);
        return View(await appDbContext.ToListAsync());
    }

    // GET: StockOrderItems/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrderItem = await _context.StockOrderItems
            .Include(s => s.Product)
            .Include(s => s.StockOrder)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stockOrderItem == null)
        {
            return NotFound();
        }

        return View(stockOrderItem);
    }

    // GET: StockOrderItems/Create
    public IActionResult Create()
    {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription");
        ViewData["StockOrderId"] = new SelectList(_context.StockOrders, "Id", "Status");
        return View();
    }

    // POST: StockOrderItems/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("StockOrderId,ProductId,Quantity,Cost,CreatedAt,UpdatedAt,Id")] StockOrderItem stockOrderItem)
    {
        if (ModelState.IsValid)
        {
            stockOrderItem.Id = Guid.NewGuid();
            _context.Add(stockOrderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", stockOrderItem.ProductId);
        ViewData["StockOrderId"] = new SelectList(_context.StockOrders, "Id", "Status", stockOrderItem.StockOrderId);
        return View(stockOrderItem);
    }

    // GET: StockOrderItems/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);
        if (stockOrderItem == null)
        {
            return NotFound();
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", stockOrderItem.ProductId);
        ViewData["StockOrderId"] = new SelectList(_context.StockOrders, "Id", "Status", stockOrderItem.StockOrderId);
        return View(stockOrderItem);
    }

    // POST: StockOrderItems/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("StockOrderId,ProductId,Quantity,Cost,CreatedAt,UpdatedAt,Id")] StockOrderItem stockOrderItem)
    {
        if (id != stockOrderItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(stockOrderItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockOrderItemExists(stockOrderItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", stockOrderItem.ProductId);
        ViewData["StockOrderId"] = new SelectList(_context.StockOrders, "Id", "Status", stockOrderItem.StockOrderId);
        return View(stockOrderItem);
    }

    // GET: StockOrderItems/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrderItem = await _context.StockOrderItems
            .Include(s => s.Product)
            .Include(s => s.StockOrder)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stockOrderItem == null)
        {
            return NotFound();
        }

        return View(stockOrderItem);
    }

    // POST: StockOrderItems/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var stockOrderItem = await _context.StockOrderItems.FindAsync(id);
        if (stockOrderItem != null)
        {
            _context.StockOrderItems.Remove(stockOrderItem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StockOrderItemExists(Guid id)
    {
        return _context.StockOrderItems.Any(e => e.Id == id);
    }
}