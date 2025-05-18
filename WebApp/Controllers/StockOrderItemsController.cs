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
public class StockOrderItemsController : Controller
{
    private readonly IAppBll _bll;
    private readonly StockOrderItemViewModelMapper _mapper = new StockOrderItemViewModelMapper();
    

    /// <inheritdoc />
    public StockOrderItemsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {

        var dtos = (await _bll.StockOrderItemService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new StockOrderItemViewModel()
        {
            StockOrderItems = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.StockOrderItemService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(_mapper.Map(entity));
    }

    private async Task PopulateStockOrdersAndProducts(
        Guid? selectedStockOrder = null,
        Guid? selectedProduct    = null)
    {
        var stockOrders = await _bll.StockOrderService.AllAsync(User.GetUserId());
        var products    = await _bll.ProductService.AllAsync(    User.GetUserId());

        ViewBag.StockOrderId = new SelectList(
            stockOrders,
            nameof(StockOrderBllDto.Id),
            nameof(StockOrderBllDto.Id),           
            selectedStockOrder);

        ViewBag.ProductId = new SelectList(
            products,
            nameof(ProductBllDto.Id),
            nameof(ProductBllDto.ProductName),
            selectedProduct);
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulateStockOrdersAndProducts();
        return View(new StockOrderItemMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StockOrderItemMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateStockOrdersAndProducts(entity.StockOrderId, entity.ProductId);
            return View(entity);
        }

        _bll.StockOrderItemService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.StockOrderItemService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulateStockOrdersAndProducts(entity.StockOrderId, entity.ProductId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, StockOrderItemMvcDto entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateStockOrdersAndProducts(entity.StockOrderId, entity.ProductId);
            return View(entity);
        }

        _bll.StockOrderItemService.Update(_mapper.Map(entity));
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.StockOrderItemService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        return View(_mapper.Map(entity));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.StockOrderItemService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}