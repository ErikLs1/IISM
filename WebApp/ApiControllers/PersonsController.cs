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
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        return (await _uow.PersonRepository.AllAsync(User.GetUserId())).ToList();
    }

    // GET: api/Persons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
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
    public async Task<IActionResult> PutPerson(Guid id, PersonDto person)
    {
        if (id != person.Id)
        {
            return BadRequest();
        }

        _uow.PersonRepository.Update(person);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Persons
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PersonDto>> PostPerson(PersonDto person)
    {
        _uow.PersonRepository.Add(person);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new { id = person.Id }, person);
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