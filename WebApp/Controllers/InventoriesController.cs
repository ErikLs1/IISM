using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class InventoriesController : Controller
{
    private readonly IAppUow _uow;

    public InventoriesController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Inventories
    public async Task<IActionResult> Index()
    {
        var res = new InventoryIndexViewModel()
        {
            Inventories = (await _uow.InventoryRepository.AllAsync(User.GetUserId())).ToList()
        };
        return View(res);
    }

    // GET: Inventories/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        
        var entity = await _uow.InventoryRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Inventories/Create
    public IActionResult Create()
    {
        
        return View();
    }

    // POST: Inventories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.InventoryRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Inventories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.InventoryRepository.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }
        
        return View(entity);
    }

    // POST: Inventories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, InventoryDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.InventoryRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Inventories/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.InventoryRepository.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Inventories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.InventoryRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}