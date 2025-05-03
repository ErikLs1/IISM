using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly IAppUow _uow;

    public CategoriesController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var res = new CategoryIndexViewModel()
        {
            Categories = (await _uow.CategoryRepository.AllAsync(User.GetUserId())).ToList(),
        };
        return View(res);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.CategoryRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.CategoryRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.CategoryRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }
        
        return View(entity);
    }

    // POST: Categories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, CategoryDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.CategoryRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.CategoryRepository.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.CategoryRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}