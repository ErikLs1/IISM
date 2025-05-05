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
public class PaymentsController : Controller
{
    private readonly AppDbContext _context;

    public PaymentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Payments
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Payments.Include(p => p.Order);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Admin/Payments/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // GET: Admin/Payments/Create
    public IActionResult Create()
    {
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CreatedBy");
        return View();
    }

    // POST: Admin/Payments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,PaymentMethod,PaymentStatus,PaymentAmount,PaymentDate,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Payment payment)
    {
        if (ModelState.IsValid)
        {
            payment.Id = Guid.NewGuid();
            _context.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CreatedBy", payment.OrderId);
        return View(payment);
    }

    // GET: Admin/Payments/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CreatedBy", payment.OrderId);
        return View(payment);
    }

    // POST: Admin/Payments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,PaymentMethod,PaymentStatus,PaymentAmount,PaymentDate,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Payment payment)
    {
        if (id != payment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(payment.Id))
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
        ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "CreatedBy", payment.OrderId);
        return View(payment);
    }

    // GET: Admin/Payments/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var payment = await _context.Payments
            .Include(p => p.Order)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (payment == null)
        {
            return NotFound();
        }

        return View(payment);
    }

    // POST: Admin/Payments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment != null)
        {
            _context.Payments.Remove(payment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentExists(Guid id)
    {
        return _context.Payments.Any(e => e.Id == id);
    }
}