using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;
using WebApp.Models.Index.Mappers;
using WebApp.Models.Index.MvcDto;
using WebApp.Models.Index.ViewModel;

namespace WebApp.Controllers;

/// <inheritdoc />
[Authorize(Roles = "manager")]
public class OrderProductsController : Controller
{
    private readonly IAppBll _bll;
    private readonly OrderProductViewModelMapper _mapper = new OrderProductViewModelMapper();

    /// <inheritdoc />
    public OrderProductsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.OrderProductService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new OrderProductViewModel()
        {
            OrderProducts = items
        };
        return View(res);
    }

    /*public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.OrderProductService.FindAsync(id.Value, User.GetUserId());
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
    public async Task<IActionResult> Create(OrderProductMvcDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.OrderProductService.Add(entity, User.GetUserId());
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

        var entity = await _bll.OrderProductService.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, OrderProductMvcDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.OrderProductService.Update(entity);
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

        var entity = await _bll.OrderProductService.FindAsync(id.Value, User.GetUserId());
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
        await _bll.OrderProductService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }*/
}