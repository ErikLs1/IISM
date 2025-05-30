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
public class WarehousesController : Controller
{
    private readonly AppDbContext _context;

    public WarehousesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Warehouses
    public async Task<IActionResult> Index()
    {
        return View(await _context.Warehouses.ToListAsync());
    }

    // GET: Admin/Warehouses/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var warehouse = await _context.Warehouses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (warehouse == null)
        {
            return NotFound();
        }

        return View(warehouse);
    }

    // GET: Admin/Warehouses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Warehouses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("WarehouseAddress,WarehouseEmail,WarehouseCapacity,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Warehouse warehouse)
    {
        if (ModelState.IsValid)
        {
            warehouse.Id = Guid.NewGuid();
            _context.Add(warehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(warehouse);
    }

    // GET: Admin/Warehouses/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null)
        {
            return NotFound();
        }
        return View(warehouse);
    }

    // POST: Admin/Warehouses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("WarehouseAddress,WarehouseEmail,WarehouseCapacity,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Warehouse warehouse)
    {
        if (id != warehouse.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(warehouse);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(warehouse.Id))
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
        return View(warehouse);
    }

    // GET: Admin/Warehouses/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var warehouse = await _context.Warehouses
            .FirstOrDefaultAsync(m => m.Id == id);
        if (warehouse == null)
        {
            return NotFound();
        }

        return View(warehouse);
    }

    // POST: Admin/Warehouses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse != null)
        {
            _context.Warehouses.Remove(warehouse);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool WarehouseExists(Guid id)
    {
        return _context.Warehouses.Any(e => e.Id == id);
    }
}