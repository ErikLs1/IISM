using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class InventoriesController : Controller
{
    private readonly AppDbContext _context;

    public InventoriesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Inventories
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Inventories.Include(i => i.Product).Include(i => i.Warehouse);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Inventories/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inventory = await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inventory == null)
        {
            return NotFound();
        }

        return View(inventory);
    }

    // GET: Inventories/Create
    public IActionResult Create()
    {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription");
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress");
        return View();
    }

    // POST: Inventories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,WarehouseId,Quantity,CreatedAt,UpdatedAt,Id")] Inventory inventory)
    {
        if (ModelState.IsValid)
        {
            inventory.Id = Guid.NewGuid();
            _context.Add(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", inventory.ProductId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", inventory.WarehouseId);
        return View(inventory);
    }

    // GET: Inventories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", inventory.ProductId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", inventory.WarehouseId);
        return View(inventory);
    }

    // POST: Inventories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,WarehouseId,Quantity,CreatedAt,UpdatedAt,Id")] Inventory inventory)
    {
        if (id != inventory.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(inventory.Id))
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
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", inventory.ProductId);
        ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "WarehouseAddress", inventory.WarehouseId);
        return View(inventory);
    }

    // GET: Inventories/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inventory = await _context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inventory == null)
        {
            return NotFound();
        }

        return View(inventory);
    }

    // POST: Inventories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var inventory = await _context.Inventories.FindAsync(id);
        if (inventory != null)
        {
            _context.Inventories.Remove(inventory);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InventoryExists(Guid id)
    {
        return _context.Inventories.Any(e => e.Id == id);
    }
}