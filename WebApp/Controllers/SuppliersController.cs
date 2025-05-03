using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class SuppliersController : Controller
{
    private readonly IAppUow _uow;

    public SuppliersController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Suppliers
    public async Task<IActionResult> Index()
    {
        var res = new SupplierIndexViewModel()
        {
            Suppliers = (await _uow.SupplierRepository.AllAsync(User.GetUserId())).ToList(),
        };
        
        return View(res);
    }

    // GET: Suppliers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.SupplierRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
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
    public async Task<IActionResult> Create(SupplierDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.SupplierRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();;
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Suppliers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.SupplierRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Suppliers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, SupplierDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.SupplierRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Suppliers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.SupplierRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Suppliers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.PersonRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}