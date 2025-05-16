using System.Security.Claims;
using App.BLL.Contracts;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonsController : ControllerBase
{

    private readonly IAppBll _bll;
    
    /// <inheritdoc />
    public PersonsController(IAppBll bll)
    {
        _bll = bll;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType( typeof( ProfileInfoDto ), StatusCodes.Status200OK)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<ProfileInfoDto>> GetProfileInfo()
    {
        var userId = User.GetUserId();
        var bll = await _bll.PersonService.GetProfileAsync(userId);
        var res = new ProfileInfoDto()
        {
            UserId = User.GetUserId(),
            PersonFirstName = bll.PersonFirstName,
            PersonLastName = bll.PersonLastName,
            PersonPhoneNumber = bll.PersonPhoneNumber,
            PersonAddress = bll.PersonAddress,
            PersonGender = bll.PersonGender,
            PersonDateOfBirth = bll.PersonBirthDate,
            Email = User.FindFirstValue(ClaimTypes.Email)!
        };
        
        return res;
    }
}