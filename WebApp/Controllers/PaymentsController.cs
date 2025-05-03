using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class PaymentsController : Controller
{
    private readonly IAppUow _uow;

    public PaymentsController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Payments
    public async Task<IActionResult> Index()
    {
        var res = new PaymentIndexViewModel()
        {
            Payments = (await _uow.PaymentRepository.AllAsync(User.GetUserId())).ToList(),
        };
        
        return View(res);
    }

    // GET: Payments/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.PaymentRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Payments/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Payments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PaymentDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.PaymentRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Payments/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.PaymentRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Payments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PaymentDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.PaymentRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Payments/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.PaymentRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Payments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.PaymentRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}