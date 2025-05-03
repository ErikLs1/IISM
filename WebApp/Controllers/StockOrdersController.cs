using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class StockOrdersController : Controller
{
    private readonly IAppUow _uow;

    public StockOrdersController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: StockOrders
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.StockOrders.Include(s => s.Supplier).Include(s => s.Warehouse);
        return View(await appDbContext.ToListAsync());
    }

    // GET: StockOrders/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrder = await _context.StockOrders
            .Include(s => s.Supplier)
            .Include(s => s.Warehouse)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stockOrder == null)
        {
            return NotFound();
        }

        return View(stockOrder);
    }

    // GET: StockOrders/Create
    public IActionResult Create()
    {
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierAddress");
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress");
        return View();
    }

    // POST: StockOrders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SupplierId,WarehouseId,TotalCost,Status,CreatedAt,UpdatedAt,Id")] StockOrder stockOrder)
    {
        if (ModelState.IsValid)
        {
            stockOrder.Id = Guid.NewGuid();
            _context.Add(stockOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierAddress", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // GET: StockOrders/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrder = await _context.StockOrders.FindAsync(id);
        if (stockOrder == null)
        {
            return NotFound();
        }
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierAddress", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // POST: StockOrders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("SupplierId,WarehouseId,TotalCost,Status,CreatedAt,UpdatedAt,Id")] StockOrder stockOrder)
    {
        if (id != stockOrder.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(stockOrder);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockOrderExists(stockOrder.Id))
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
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "SupplierAddress", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // GET: StockOrders/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var stockOrder = await _context.StockOrders
            .Include(s => s.Supplier)
            .Include(s => s.Warehouse)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (stockOrder == null)
        {
            return NotFound();
        }

        return View(stockOrder);
    }

    // POST: StockOrders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var stockOrder = await _context.StockOrders.FindAsync(id);
        if (stockOrder != null)
        {
            _context.StockOrders.Remove(stockOrder);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StockOrderExists(Guid id)
    {
        return _context.StockOrders.Any(e => e.Id == id);
    }
}