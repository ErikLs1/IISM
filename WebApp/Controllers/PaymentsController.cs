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
public class PaymentsController : Controller
{
    private readonly IAppBll _bll;
    private readonly PaymentViewModelMapper _mapper = new PaymentViewModelMapper();
    

    /// <inheritdoc />
    public PaymentsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.PaymentService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new PaymentViewModel()
        {
            Payments = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.PaymentService.FindAsync(id.Value, User.GetUserId());
        
        if (entity == null)
        {
            return NotFound();
        }

        return View(_mapper.Map(entity));
    }

    private async Task PopulateOrders(Guid? selectedOrder = null)
    {
        var orders = await _bll.OrderService.AllAsync(User.GetUserId());
        // value = Id, text = ShippingAddress
        ViewBag.OrderId = new SelectList(
            orders,
            nameof(OrderBllDto.Id),
            nameof(OrderBllDto.OrderShippingAddress),
            selectedOrder
        );
    }
    
    public async Task<IActionResult> Create()
    {
        await PopulateOrders();
        return View(new PaymentMvcDto());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PaymentMvcDto entity)
    {
        if (!ModelState.IsValid)
        {
            await PopulateOrders(entity.OrderId);
            return View(entity);
        }

        _bll.PaymentService.Add(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null) return NotFound();
        var entity = await _bll.PaymentService.FindAsync(id.Value, User.GetUserId());
        if (entity == null) return NotFound();
        await PopulateOrders(entity.OrderId);
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PaymentMvcDto entity)
    {
        if (id != entity.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateOrders(entity.OrderId);
            return View(entity);
        }

        _bll.PaymentService.Update(_mapper.Map(entity), User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.PaymentService.FindAsync(id.Value, User.GetUserId());
        
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
        await _bll.PaymentService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}