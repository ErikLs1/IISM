using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class StockOrdersController : Controller
{
    private readonly IAppUow _uow;

    public StockOrdersController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: StockOrders
    public async Task<IActionResult> Index()
    {
        var res = new StockOrderIndexViewModel()
        {
            StockOrders = (await _uow.StockOrderRepository.AllAsync(User.GetUserId())).ToList()
        };
        
        return View(res);
    }

    // GET: StockOrders/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.StockOrderRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: StockOrders/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: StockOrders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StockOrderDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.StockOrderRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: StockOrders/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.StockOrderRepository.FindAsync(id.Value, User.GetUserId());

        
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: StockOrders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, StockOrderDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.StockOrderRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: StockOrders/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.StockOrderRepository.FindAsync(id.Value, User.GetUserId());

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: StockOrders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.StockOrderRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}