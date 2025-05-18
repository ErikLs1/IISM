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
public class OrdersController : Controller
{
    private readonly IAppBll _bll;
    private readonly OrderViewModelMapper _mapper = new OrderViewModelMapper();
    
    
    /// <inheritdoc />
    public OrdersController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.OrderService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new OrderViewModel()
        {
            Orders = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.OrderService.FindAsync(id.Value);
        if (entity == null) return NotFound();
        return View(_mapper.Map(entity));
    }

    private async Task PopulatePersons(Guid? selectedPerson = null)
    {
        var people = await _bll.PersonService.AllAsync();
        ViewBag.PersonId = new SelectList(
            people,
            nameof(PersonBllDto.Id),
            nameof(PersonBllDto.PersonFirstName),
            selectedPerson
        );
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulatePersons();
        return View(new OrderMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulatePersons(entity.PersonId);
            return View(entity);
        }

        _bll.OrderService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.OrderService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulatePersons(entity.PersonId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, OrderMvcDto entity)
    {
        if (id != entity.Id) return NotFound();
        
        if (!ModelState.IsValid)
        {
            await PopulatePersons(entity.PersonId);
            return View(entity);
        }
        
        _bll.OrderService.Update(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.OrderService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        return View(_mapper.Map(entity));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.OrderService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}