using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly IAppUow _uow;

    public ProductsController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        var res = new ProductIndexViewModel()
        {
            Products = (await _uow.ProductRepository.AllAsync(User.GetUserId())).ToList()
        };
        
        return View(res);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.ProductRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.ProductRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.ProductRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Products/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.ProductRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.ProductRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.ProductRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}