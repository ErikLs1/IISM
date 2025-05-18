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
public class RefundsController : Controller
{
    private readonly IAppBll _bll;
    private readonly RefundViewModelMapper _mapper = new RefundViewModelMapper();
    

    /// <inheritdoc />
    public RefundsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.RefundService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new RefundViewModel()
        {
            Refunds = items
        };
        return View(res);
    }

     public async Task<IActionResult> Details(Guid? id)
     {
         if (id == null)
         {
             return NotFound();
         }

         var entity = await _bll.RefundService.FindAsync(id.Value, User.GetUserId());

         if (entity == null)
         {
             return NotFound();
         }

         return View(_mapper.Map(entity));
     }

     private async Task PopulateOrderProducts(Guid? selectedId = null)
     {
         var ops = await _bll.OrderProductService.AllAsync(User.GetUserId());

         ViewBag.OrderProductId = new SelectList(
             ops,
             nameof(OrderProductBllDto.Id),
             nameof(OrderProductBllDto.Id),
             selectedId
         );
     }
     public async Task<IActionResult> Create()
     {
         await PopulateOrderProducts();
         return View(new RefundMvcDto());
     }
     
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Create(RefundMvcDto entity)
     {
         if (!ModelState.IsValid)
         {
             await PopulateOrderProducts(entity.OrderProductId);
             return View(entity);
         }
         _bll.RefundService.Add(_mapper.Map(entity), User.GetUserId());
         await _bll.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
     }

     public async Task<IActionResult> Edit(Guid? id)
     {
         if (id == null) return NotFound();
         var entity = await _bll.RefundService.FindAsync(id.Value, User.GetUserId());
         if (entity == null) return NotFound();
         await PopulateOrderProducts(entity.OrderProductId);
         return View(_mapper.Map(entity));
     }
     
     [HttpPost]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> Edit(Guid id, RefundMvcDto entity)
     {
         if (id != entity.Id) return NotFound();
         if (!ModelState.IsValid)
         {
             await PopulateOrderProducts(entity.OrderProductId);
             return View(entity);
         }
         
         _bll.RefundService.Update(_mapper.Map(entity));
         await _bll.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
     }

     public async Task<IActionResult> Delete(Guid? id)
     {
         if (id == null) return NotFound();
         var entity = await _bll.RefundService.FindAsync(id.Value, User.GetUserId());
         if (entity == null) return NotFound();
         return View(_mapper.Map(entity));
     }

     [HttpPost, ActionName("Delete")]
     [ValidateAntiForgeryToken]
     public async Task<IActionResult> DeleteConfirmed(Guid id)
     {
         await _bll.RefundService.RemoveAsync(id, User.GetUserId());
         await _bll.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
     }
}