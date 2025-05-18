using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> Details(Guid? id)
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

        return View(_mapper.Map(entity));
    }

    private async Task PopulateOrdersAndProducts(Guid? selectedOrder = null,
        Guid? selectedProduct = null)
    {
        var orders   = await _bll.OrderService.AllAsync(User.GetUserId());
        var products = await _bll.ProductService.AllAsync(User.GetUserId());

        ViewBag.OrderId = new SelectList(
            orders,
            nameof(OrderBllDto.Id),
            nameof(OrderBllDto.Id),
            selectedOrder);

        ViewBag.ProductId = new SelectList(
            products,
            nameof(ProductBllDto.Id),
            nameof(ProductBllDto.ProductName),
            selectedProduct);
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulateOrdersAndProducts();
        return View(new OrderProductMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderProductMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateOrdersAndProducts(entity.OrderId, entity.ProductId);
            return View(entity);
        }
        _bll.OrderProductService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.OrderProductService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulateOrdersAndProducts(entity.OrderId, entity.ProductId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, OrderProductMvcDto entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateOrdersAndProducts(entity.OrderId, entity.ProductId);
            return View(entity);
        }
        _bll.OrderProductService.Update(_mapper.Map(entity));
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
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

        return View(_mapper.Map(entity));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.OrderProductService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}