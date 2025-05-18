using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index.Mappers;
using WebApp.Models.Index.MvcDto;
using WebApp.Models.Index.ViewModel;

namespace WebApp.Controllers;

/// <inheritdoc />
[Authorize(Roles = "manager")]
public class ProductsController : Controller
{
    private readonly IAppBll _bll;
    private readonly ProductViewModelMapper _mapper = new ProductViewModelMapper();
    

    /// <inheritdoc />
    public ProductsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.ProductService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new ProductViewModel()
        {
            Products = items
        };
        return View(res);
    }

    /*public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductMvcDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.ProductService.Add(entity, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductMvcDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.ProductService.Update(entity);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.ProductService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.ProductService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }*/
}