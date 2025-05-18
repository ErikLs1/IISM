using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.Index;
using WebApp.Models.Index.Mappers;
using WebApp.Models.Index.MvcDto;
using WebApp.Models.Index.ViewModel;

namespace WebApp.Controllers;

/// <inheritdoc />
[Authorize(Roles = "manager")]
public class StockOrdersController : Controller
{
    private readonly IAppBll _bll;
    private readonly StockOrderViewModelMapper _mapper = new StockOrderViewModelMapper();
    

    /// <inheritdoc />
    public StockOrdersController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.StockOrderService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new StockOrderViewModel()
        {
            StockOrders = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.StockOrderService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(_mapper.Map(entity));
    }

    private async Task PopulateSuppliersAndWarehouses(
        Guid? selectedSupplier  = null,
        Guid? selectedWarehouse = null)
    {
        var suppliers  = await _bll.SupplierService.AllAsync(User.GetUserId());
        var warehouses = await _bll.WarehouseService.AllAsync(User.GetUserId());

        ViewBag.SupplierId = new SelectList(
            suppliers,
            nameof(SupplierBllDto.Id),
            nameof(SupplierBllDto.SupplierName),
            selectedSupplier);

        ViewBag.WarehouseId = new SelectList(
            warehouses,
            nameof(WarehouseBllDto.Id),
            nameof(WarehouseBllDto.WarehouseAddress),
            selectedWarehouse);
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulateSuppliersAndWarehouses();
        return View(new StockOrderMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StockOrderMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSuppliersAndWarehouses(entity.SupplierId, entity.WarehouseId);
            return View(entity);
        }
        
        _bll.StockOrderService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.StockOrderService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulateSuppliersAndWarehouses(entity.SupplierId, entity.WarehouseId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, StockOrderMvcDto entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateSuppliersAndWarehouses(entity.SupplierId, entity.WarehouseId);
            return View(entity);
        }
        _bll.StockOrderService.Update(_mapper.Map(entity));
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.StockOrderService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        return View(_mapper.Map(entity));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.StockOrderService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}