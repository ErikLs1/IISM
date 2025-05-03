using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index;

namespace WebApp.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly IAppUow _uow;

    public OrdersController(IAppUow uow)
    {
        _uow = uow;
    }

    // GET: Orders
    public async Task<IActionResult> Index()
    {
        var res = new OrderIndexViewModel()
        {
            Orders = (await _uow.OrderRepository.AllAsync(User.GetUserId())).ToList()
        };
        return View(res);
    }

    // GET: Orders/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.OrderRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // GET: Orders/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Orders/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderDalDto entity)
    {
        if (ModelState.IsValid)
        {
            _uow.OrderRepository.Add(entity, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Orders/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.OrderRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }

    // POST: Orders/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, OrderDalDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _uow.OrderRepository.Update(entity);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    // GET: Orders/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _uow.OrderRepository.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _uow.OrderRepository.RemoveAsync(id, User.GetUserId());
        await _uow.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}