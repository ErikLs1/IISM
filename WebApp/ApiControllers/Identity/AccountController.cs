using App.BLL.Services;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using App.DTO.V1.Mappers;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// User account controller with login, register functionality.
/// </summary>
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly AccountMapper _accountMapper;


    public AccountController(AccountService accountService, AccountMapper accountMapper)
    {
        _accountService = accountService;
        _accountMapper = accountMapper;
    }

    /// <summary>
    /// User authentication, returns JWT and refresh token
    /// </summary>
    /// <param name="loginInfo">Login model</param>
    /// <param name="jwtExpiresInSeconds">Custom jwt expiration</param>
    /// <param name="refreshTokenExpiresInSeconds">Custom refresh token expiration</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> Login(
        [FromBody]
        LoginDto loginInfo,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {

        var res = await _accountService.LoginAsync(_accountMapper.Map(loginInfo)!, jwtExpiresInSeconds,
            refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// Register endpoint for REST API.
    /// </summary>
    /// <param name="registerModel"></param>
    /// <param name="jwtExpiresInSeconds"></param>
    /// <param name="refreshTokenExpiresInSeconds"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> Register(
        [FromBody]
        RegisterDto registerModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds)
    {
        var res = await _accountService.RegisterAsync(_accountMapper.Map(registerModel)!, jwtExpiresInSeconds,
            refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// Refresh token renewal endpoint for rest API
    /// </summary>
    /// <param name="refreshTokenModel">Data for renewal</param>
    /// <param name="jwtExpiresInSeconds">Custom expiration for jwt</param>
    /// <param name="refreshTokenExpiresInSeconds">Custom expiration for refresh token</param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(JwtResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<JwtResponseDto>> RenewRefreshToken(
        [FromBody]
        RefreshTokenDto refreshTokenModel,
        [FromQuery]
        int? jwtExpiresInSeconds,
        [FromQuery]
        int? refreshTokenExpiresInSeconds
    )
    {
        var bllDto = _accountMapper.Map(refreshTokenModel)!;
        var res = await _accountService.RenewTokenAsync(bllDto, jwtExpiresInSeconds, refreshTokenExpiresInSeconds);
        return Ok(_accountMapper.Map(res));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logout"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status404NotFound)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult> Logout([FromBody] LogoutDto logout)
    {
        var userId = User.GetUserId();
        await _accountService.LogoutAsync(userId, logout.RefreshToken);
        return Ok();
    }
}