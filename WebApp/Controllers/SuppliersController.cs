using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class SuppliersController : Controller
{
    private readonly AppDbContext _context;

    public SuppliersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Suppliers
    public async Task<IActionResult> Index()
    {
        return View(await _context.Suppliers.ToListAsync());
    }

    // GET: Suppliers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supplier == null)
        {
            return NotFound();
        }

        return View(supplier);
    }

    // GET: Suppliers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Suppliers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SupplierName,SupplierPhoneNumber,SupplierEmail,SupplierAddress,CreatedAt,UpdatedAt,Id")] Supplier supplier)
    {
        if (ModelState.IsValid)
        {
            supplier.Id = Guid.NewGuid();
            _context.Add(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(supplier);
    }

    // GET: Suppliers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null)
        {
            return NotFound();
        }
        return View(supplier);
    }

    // POST: Suppliers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("SupplierName,SupplierPhoneNumber,SupplierEmail,SupplierAddress,CreatedAt,UpdatedAt,Id")] Supplier supplier)
    {
        if (id != supplier.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(supplier.Id))
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
        return View(supplier);
    }

    // GET: Suppliers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var supplier = await _context.Suppliers
            .FirstOrDefaultAsync(m => m.Id == id);
        if (supplier == null)
        {
            return NotFound();
        }

        return View(supplier);
    }

    // POST: Suppliers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SupplierExists(Guid id)
    {
        return _context.Suppliers.Any(e => e.Id == id);
    }
}