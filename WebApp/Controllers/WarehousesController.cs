using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;
using WebApp.Models.Index.Mappers;
using WebApp.Models.Index.ViewModel;

namespace WebApp.Controllers;

/// <inheritdoc />
[Authorize(Roles = "manager")]
public class WarehousesController : Controller
{
    private readonly IAppBll _bll;
    private readonly WarehouseViewModelMapper _mapper = new WarehouseViewModelMapper();

    /// <inheritdoc />
    public WarehousesController(IAppBll uow)
    {
        _bll = uow;
    }

    // GET: Warehouses
    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.WarehouseService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new WarehouseViewModel()
        {
            Warehouses = items
        };
        return View(res);
    }

    // GET: Warehouses/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.WarehouseService.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Warehouses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Warehouses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WarehouseBllDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.WarehouseService.Add(entity, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Warehouses/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.WarehouseService.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Warehouses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, WarehouseBllDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.WarehouseService.Update(entity);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Warehouses/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.WarehouseService.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Warehouses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.WarehouseService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}