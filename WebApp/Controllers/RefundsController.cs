using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class RefundsController : Controller
{
    private readonly IAppUow _uow;

    public RefundsController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Refunds
    public async Task<IActionResult> Index()
    {
        var res = new RefundIndexViewModel()
        {
            Refunds = (await _uow.RefundRepository.AllAsync(User.GetUserId())).ToList(),
        };
        
        return View(res);
    }

    // GET: Refunds/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.RefundRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Refunds/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Refunds/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RefundDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.RefundRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Refunds/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.RefundRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Refunds/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, RefundDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.RefundRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Refunds/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.RefundRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Refunds/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.RefundRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}