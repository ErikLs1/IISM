using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain;
using Base.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IAppUow _uow;
        public PersonsController(IAppUow uow)
        {
            _uow = uow;
        }

        // GET: Persons
        public async Task<IActionResult> Index()
        {
            var res = new PersonIndexViewModel()
            {
                Persons = (await _uow.PersonRepository.AllAsync(User.GetUserId())).ToList(),
                PersonCountByName = await _uow.PersonRepository.GetPersonCountByNameAsync("Bob", User.GetUserId())
            };
            return View(res);
        }

        // GET: Persons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());
            
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // GET: Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person entity)
        {
            entity.UserId = User.GetUserId();
            
            if (ModelState.IsValid)
            {
                _uow.PersonRepository.Add(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Person entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                entity.UserId = User.GetUserId();
                _uow.PersonRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Persons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _uow.PersonRepository.FindAsync(id.Value, User.GetUserId());
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        // POST: Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PersonRepository.RemoveAsync(id, User.GetUserId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
