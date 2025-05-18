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
public class ProductSuppliersController : Controller
{
    private readonly IAppBll _bll;
    private readonly ProductSupplierViewModelMapper _mapper = new ProductSupplierViewModelMapper();
    

    /// <inheritdoc />
    public ProductSuppliersController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.ProductSupplierService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new ProductSupplierViewModel()
        {
            ProductSuppliers = items
        };
        return View(res);
    }

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

        return View(_mapper.Map(entity));
    }

    private async Task PopulateSuppliersAndProducts(Guid? selectedSupplier = null,
        Guid? selectedProduct  = null)
    {
        // 1) fetch all suppliers & products
        var suppliers = await _bll.SupplierService.AllAsync(User.GetUserId());
        var products  = await _bll.ProductService.AllAsync( User.GetUserId());

        // 2) build SelectLists of (value: Id, text: Name)
        ViewBag.SupplierId =
            new SelectList(suppliers, nameof(SupplierBllDto.Id), nameof(SupplierBllDto.SupplierName), selectedSupplier);

        ViewBag.ProductId  =
            new SelectList(products,  nameof(ProductBllDto.Id),  nameof(ProductBllDto.ProductName),  selectedProduct);
    }
    
    public async Task<IActionResult> Create()
    { 
        await PopulateSuppliersAndProducts();
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductSupplierMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSuppliersAndProducts(entity.SupplierId, entity.ProductId);
            return View(entity);
        }
        
        _bll.ProductSupplierService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.ProductSupplierService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        
        var vm = _mapper.Map(entity);
        await PopulateSuppliersAndProducts(vm.SupplierId, vm.ProductId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProductSupplierMvcDto entity)
    {
        if (id != entity.Id) return NotFound();
        

        if (!ModelState.IsValid)
        {
            await PopulateSuppliersAndProducts(entity.SupplierId, entity.ProductId);
            return View(entity);
        }
        
        _bll.ProductSupplierService.Update(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

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

        return View(_mapper.Map(entity));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.ProductSupplierService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}