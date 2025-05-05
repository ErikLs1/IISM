using App.BLL.Contracts;
using App.DTO.V1;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <summary>
/// 
/// </summary>
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonsController : ControllerBase
{

    private readonly IAppBll _bll;

    public PersonsController(IAppBll bll)
    {
        _bll = bll;
    }


    /// <summary>
    /// Get all persons for currently logged in user
    /// </summary>
    /// <returns>List of persons</returns>
    [HttpGet]
    [Produces( "application/json" )]
    [ProducesResponseType( typeof( PersonDto ), 200 )]
    [ProducesResponseType( 404 )]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons()
    {
        var data = (await _bll.PersonService.AllAsync(User.GetUserId())).ToList();
        var res = data.Select(p => new PersonDto()
        {
            Id = p.Id,
            PersonFirstName = p.PersonFirstName,
            PersonLastName = p.PersonLastName
        }).ToList();
        return res;
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
    {
        var person = await _bll.PersonService.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }
        var res = new PersonDto()
        {
            Id = person.Id,
            PersonFirstName = person.PersonFirstName,
            PersonLastName = person.PersonLastName
        };
        return res;
    }

    
    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutPerson(Guid id, PersonDto person)
    // {
    //     if (id != person.Id)
    //     {
    //         return BadRequest();
    //     }
    //     // TODO Add create person dto
    //     _bll.PersonService.Update(person);
    //     await _bll.SaveChangesAsync();
    //     return NoContent();
    // }
    //
    //
    // [HttpPost]
    // public async Task<ActionResult<PersonDalDto>> PostPerson(PersonDalDto personDal)
    // {
    //     _bll.PersonService.Add(personDal);
    //     await _bll.SaveChangesAsync();
    //
    //     return CreatedAtAction("GetPerson", new
    //     {
    //         id = personDal.Id,
    //         version = HttpContext.GetRequestedApiVersion()!.ToString()
    //     }, personDal);
    // }
    //
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeletePerson(Guid id)
    // {
    //     var person = await _bll.PersonService.FindAsync(id);
    //     if (person == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _bll.PersonService.Remove(person);
    //     await _bll.SaveChangesAsync();
    //
    //     return NoContent();
    // }
}