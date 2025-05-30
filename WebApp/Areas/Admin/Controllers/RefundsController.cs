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
public class RefundsController : Controller
{
    private readonly AppDbContext _context;

    public RefundsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Refunds
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Refunds.Include(r => r.OrderProduct);
        return View(await appDbContext.ToListAsync());
    }

    // GET: Admin/Refunds/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var refund = await _context.Refunds
            .Include(r => r.OrderProduct)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (refund == null)
        {
            return NotFound();
        }

        return View(refund);
    }

    // GET: Admin/Refunds/Create
    public IActionResult Create()
    {
        ViewData["OrderProductId"] = new SelectList(_context.OrderProducts, "Id", "CreatedBy");
        return View();
    }

    // POST: Admin/Refunds/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderProductId,RefundAmount,RefundReason,RefundStatus,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Refund refund)
    {
        if (ModelState.IsValid)
        {
            refund.Id = Guid.NewGuid();
            _context.Add(refund);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["OrderProductId"] = new SelectList(_context.OrderProducts, "Id", "CreatedBy", refund.OrderProductId);
        return View(refund);
    }

    // GET: Admin/Refunds/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var refund = await _context.Refunds.FindAsync(id);
        if (refund == null)
        {
            return NotFound();
        }
        ViewData["OrderProductId"] = new SelectList(_context.OrderProducts, "Id", "CreatedBy", refund.OrderProductId);
        return View(refund);
    }

    // POST: Admin/Refunds/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("OrderProductId,RefundAmount,RefundReason,RefundStatus,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt,SysNotes")] Refund refund)
    {
        if (id != refund.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(refund);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefundExists(refund.Id))
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
        ViewData["OrderProductId"] = new SelectList(_context.OrderProducts, "Id", "CreatedBy", refund.OrderProductId);
        return View(refund);
    }

    // GET: Admin/Refunds/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var refund = await _context.Refunds
            .Include(r => r.OrderProduct)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (refund == null)
        {
            return NotFound();
        }

        return View(refund);
    }

    // POST: Admin/Refunds/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var refund = await _context.Refunds.FindAsync(id);
        if (refund != null)
        {
            _context.Refunds.Remove(refund);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RefundExists(Guid id)
    {
        return _context.Refunds.Any(e => e.Id == id);
    }
}