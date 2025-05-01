using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class OrderProductsController : Controller
{
    private readonly AppDbContext _context;

    public OrderProductsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: OrderProducts
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.OrderProducts.Include(o => o.Order).Include(o => o.Product);
        return View(await appDbContext.ToListAsync());
    }

    // GET: OrderProducts/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _context.OrderProducts
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (orderProduct == null)
        {
            return NotFound();
        }

        return View(orderProduct);
    }

    // GET: OrderProducts/Create
    public IActionResult Create()
    {
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderShippingAddress");
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription");
        return View();
    }

    // POST: OrderProducts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,OrderId,Quantity,TotalPrice,CreatedAt,UpdatedAt,Id")] OrderProduct orderProduct)
    {
        if (ModelState.IsValid)
        {
            orderProduct.Id = Guid.NewGuid();
            _context.Add(orderProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderShippingAddress", orderProduct.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", orderProduct.ProductId);
        return View(orderProduct);
    }

    // GET: OrderProducts/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _context.OrderProducts.FindAsync(id);
        if (orderProduct == null)
        {
            return NotFound();
        }
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderShippingAddress", orderProduct.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", orderProduct.ProductId);
        return View(orderProduct);
    }

    // POST: OrderProducts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,OrderId,Quantity,TotalPrice,CreatedAt,UpdatedAt,Id")] OrderProduct orderProduct)
    {
        if (id != orderProduct.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(orderProduct.Id))
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
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "OrderShippingAddress", orderProduct.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductDescription", orderProduct.ProductId);
        return View(orderProduct);
    }

    // GET: OrderProducts/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var orderProduct = await _context.OrderProducts
            .Include(o => o.Order)
            .Include(o => o.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (orderProduct == null)
        {
            return NotFound();
        }

        return View(orderProduct);
    }

    // POST: OrderProducts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var orderProduct = await _context.OrderProducts.FindAsync(id);
        if (orderProduct != null)
        {
            _context.OrderProducts.Remove(orderProduct);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderProductExists(Guid id)
    {
        return _context.OrderProducts.Any(e => e.Id == id);
    }
}