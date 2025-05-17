using App.BLL.Contracts;
using App.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index.Mappers;
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
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    // GET: Inventories/Details/5
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

        return View(entity);
    }

    // GET: Inventories/Create
    public IActionResult Create()
    {
        
        return View();
    }

    // POST: Inventories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryBllDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.InventoryService.Add(entity, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Inventories/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
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
        
        return View(entity);
    }

    // POST: Inventories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, InventoryBllDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.InventoryService.Update(entity);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Inventories/Delete/5
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

        return View(entity);
    }

    // POST: Inventories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.InventoryService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}