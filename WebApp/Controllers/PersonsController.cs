using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.Index.Mappers;
using WebApp.Models.Index.MvcDto;
using WebApp.Models.Index.ViewModel;

namespace WebApp.Controllers;

/// <inheritdoc />
[Authorize(Roles = "manager")]
public class PersonsController : Controller
{
    private readonly IAppBll _bll;
    private readonly PersonViewModelMapper _mapper = new PersonViewModelMapper();
    

    /// <inheritdoc />
    public PersonsController(IAppBll uow)
    {
        _bll = uow;
    }

    public async Task<IActionResult> Index()
    {
        var dtos = (await _bll.PersonService.AllAsync(User.GetUserId())).ToList();

        var items = dtos.Select(x => _mapper.Map(x)).ToList();
        
        var res = new PersonViewModel()
        {
            Persons = items
        };
        return View(res);
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
            
        if (entity == null)
        {
            return NotFound();
        }

        return View(_mapper.Map(entity));
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PersonMvcDto entity)
    {
        if (ModelState.IsValid)
        {
            _bll.PersonService.Add(_mapper.Map(entity), User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }
    
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
        if (entity == null)
        {
            return NotFound();
        }
        return View(_mapper.Map(entity));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, PersonMvcDto entity)
    {
        if (id != entity.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _bll.PersonService.Update(_mapper.Map(entity));
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entity);
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = await _bll.PersonService.FindAsync(id.Value, User.GetUserId());
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
        await _bll.PersonService.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}