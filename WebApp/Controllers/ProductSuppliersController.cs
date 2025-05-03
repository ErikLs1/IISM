using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class ProductSuppliersController : Controller
{
    private readonly IAppBll _bll;

    public ProductSuppliersController(IAppBll uow)
    {
        _bll = uow;
    }

    // GET: ProductSuppliers
    public async Task<IActionResult> Index()
    {
        var res = new ProductSupplierIndexViewModel()
        {
            ProductSuppliers = (await _bll.ProductSupplierService.AllAsync(User.GetUserId())).ToList(),
        };
        
        return View(res);
    }

    // GET: ProductSuppliers/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductSupplierService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: ProductSuppliers/Create
    public IActionResult Create()
    {
       return View();
    }

    // POST: ProductSuppliers/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductSupplierBllDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.ProductSupplierService.Add(entity, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        return View(entity);
    }

    // GET: ProductSuppliers/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductSupplierService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: ProductSuppliers/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductSupplierBllDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.ProductSupplierService.Update(entity);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: ProductSuppliers/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductSupplierService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: ProductSuppliers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.ProductSupplierService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}