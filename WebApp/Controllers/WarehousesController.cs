using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class WarehousesController : Controller
{
    private readonly IAppUow _uow;

    public WarehousesController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Warehouses
    public async Task<IActionResult> Index()
    {
        var res = new WarehouseIndexViewModel()
        {
            Warehouses = (await _uow.WarehouseRepository.AllAsync(User.GetUserId())).ToList()
        };
        
        return View(res);
    }

    // GET: Warehouses/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.WarehouseRepository.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Warehouses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Warehouses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WarehouseDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.WarehouseRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Warehouses/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.WarehouseRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Warehouses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WarehouseDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.WarehouseRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Warehouses/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.WarehouseRepository.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Warehouses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.WarehouseRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}