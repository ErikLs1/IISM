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
public class InventoriesController : Controller
{
    private readonly IAppBll _bll;
    private readonly InventoryViewModelMapper _mapper = new InventoryViewModelMapper();

    /// <inheritdoc />
    public InventoriesController(IAppBll uow)
    {
        _bll = uow;
    }
    
    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.InventoryService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new InventoryViewModel()
        {
           Inventories = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        
        var entity = await _bll.InventoryService.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(_mapper.Map(entity));
    }
    
    private async Task PopulateProductsAndWarehouses(
        Guid? selectedProduct   = null,
        Guid? selectedWarehouse = null)
    {
        var products   = await _bll.ProductService.AllAsync(User.GetUserId());
        var warehouses = await _bll.WarehouseService.AllAsync(User.GetUserId());

        ViewBag.ProductId = new SelectList(
            products,
            nameof(ProductBllDto.Id),
            nameof(ProductBllDto.ProductName),
            selectedProduct);

        ViewBag.WarehouseId = new SelectList(
            warehouses,
            nameof(WarehouseBllDto.Id),
            nameof(WarehouseBllDto.WarehouseAddress),
            selectedWarehouse);
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulateProductsAndWarehouses();
        return View(new InventoryMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateProductsAndWarehouses(entity.ProductId, entity.WarehouseId);
            return View(entity);
        }
        
        _bll.InventoryService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.InventoryService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulateProductsAndWarehouses(entity.ProductId, entity.WarehouseId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, InventoryMvcDto entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateProductsAndWarehouses(entity.ProductId, entity.WarehouseId);
            return View(entity);
        }

        _bll.InventoryService.Update(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.InventoryService.FindAsync(id.Value, User.GetUserId());
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
        await _bll.InventoryService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}