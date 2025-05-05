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

namespace WebApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "admin")]
public class StockOrdersController : Controller
{
    private readonly AppDbContext _context;

    public StockOrdersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/StockOrders
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.StockOrders.Include(s => s.Supplier).Include(s => s.Warehouse);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Admin/StockOrders/Details/5
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

    // GET: Admin/StockOrders/Create
    public IActionResult Create()
    {
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CreatedBy");
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "CreatedBy");
        return View();
    }

    // POST: Admin/StockOrders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SupplierId,WarehouseId,TotalCost,Status,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] StockOrder stockOrder)
    {
        if (ModelState.IsValid)
        {
            stockOrder.Id = Guid.NewGuid();
            _context.Add(stockOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CreatedBy", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "CreatedBy", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // GET: Admin/StockOrders/Edit/5
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
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CreatedBy", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "CreatedBy", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // POST: Admin/StockOrders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("SupplierId,WarehouseId,TotalCost,Status,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] StockOrder stockOrder)
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
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CreatedBy", stockOrder.SupplierId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "CreatedBy", stockOrder.WarehouseId);
        return View(stockOrder);
    }

    // GET: Admin/StockOrders/Delete/5
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

    // POST: Admin/StockOrders/Delete/5
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