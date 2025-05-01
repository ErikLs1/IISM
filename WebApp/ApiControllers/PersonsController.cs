using App.DAL.Contracts;
using App.DAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PersonsController : ControllerBase
{

    private readonly IAppUow _uow;

    public PersonsController(IAppUow uow)
    {
        _uow = uow;
    }


    // GET: api/Persons
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonDalDto>>> GetPersons()
    {
        return (await _uow.PersonRepository.AllAsync(User.GetUserId())).ToList();
    }

    // GET: api/Persons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDalDto>> GetPerson(Guid id)
    {
        var person = await _uow.PersonRepository.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return person;
    }

    // PUT: api/Persons/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(Guid id, PersonDalDto personDal)
    {
        if (id != personDal.Id)
        {
            return BadRequest();
        }

        _uow.PersonRepository.Update(personDal);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Persons
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PersonDalDto>> PostPerson(PersonDalDto personDal)
    {
        _uow.PersonRepository.Add(personDal);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new { id = personDal.Id }, personDal);
    }

    // DELETE: api/Persons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var person = await _uow.PersonRepository.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _uow.PersonRepository.Remove(person);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}