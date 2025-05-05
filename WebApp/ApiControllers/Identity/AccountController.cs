using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.Identity;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Base.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// User account controller with login, register functionality.
/// </summary>
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly Random _random = new Random();
    private readonly AppDbContext _context;

    private const string UserPassProblem = "User/Password problem";
    private const int RandomDelayMin = 500;
    private const int RandomDelayMax = 5000;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="userManager"></param>
    /// <param name="logger"></param>
    /// <param name="signInManager"></param>
    /// <param name="context"></param>
    public AccountController(IConfiguration configuration, UserManager<AppUser> userManager, 
        ILogger<AccountController> logger, SignInManager<AppUser> signInManager, AppDbContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _logger = logger;
        _signInManager = signInManager;
        _context = context;
    }

    
    /// <summary>
    /// Login endpoint for REST API.
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
        // verify user
        var appUser = await _userManager.FindByEmailAsync(loginInfo.Email);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginInfo.Email);
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new MessageDto(UserPassProblem));
        }

        // verify password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password {} for email {} was wrong", loginInfo.Password,
                loginInfo.Email);
            
            // Random delay
            await Task.Delay(_random.Next(RandomDelayMin, RandomDelayMax));
            return NotFound(new MessageDto(UserPassProblem));
        }

        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));

        
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (!_context.Database.ProviderName!.Contains("InMemory"))
        {
            var deletedRows = await _context
                .RefreshTokens
                .Where(t => t.UserId == appUser.Id && t.Expiration < DateTime.UtcNow)
                .ExecuteDeleteAsync();
            _logger.LogInformation("Deleted {} refresh tokens", deletedRows);
        }
        else
        {
            //TODO: inMemory delete for testing
        }
        

        // todo: set refresh token expiration
        var refreshToken = new AppRefreshToken()
        {
            UserId = appUser.Id,
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, "JWTSecurity:ExpiresInSeconds")
        };
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWTSecurity:Key")!,
            _configuration.GetValue<string>("JWTSecurity:Issuer")!,
            _configuration.GetValue<string>("JWTSecurity:Audience")!,
            GetExpirationDateTime(jwtExpiresInSeconds, "JWTSecurity:ExpiresInSeconds")
        );

        var responseData = new JwtResponseDto()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.RefreshToken
        };

        return Ok(responseData);
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
        // is user already registered
        var appUser = await _userManager.FindByEmailAsync(registerModel.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {User} is already registered", registerModel.Email);
            return BadRequest(new MessageDto("User already registered."));
        }

        // register user
        var refreshToken = new AppRefreshToken()
        {
            Expiration = GetExpirationDateTime(refreshTokenExpiresInSeconds, "JWTSecurity:RefreshTokenExpiresInSeconds")
        };
        appUser = new AppUser()
        {
            Email = registerModel.Email,
            UserName = registerModel.Email,
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            RefreshTokens = new List<AppRefreshToken>()
            {
                refreshToken
            }
        };

        var result = await _userManager.CreateAsync(appUser, registerModel.Password);
        
        if (result.Succeeded)
        {
            _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

            var user = await _userManager.FindByEmailAsync(appUser.Email);
            if (user != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.FirstName));
                await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, user.LastName));

                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration.GetValue<string>("JWTSecurity:Key")!,
                    _configuration.GetValue<string>("JWTSecurity:Issuer")!,
                    _configuration.GetValue<string>("JWTSecurity:Audience")!,
                    GetExpirationDateTime(jwtExpiresInSeconds, "JWTSecurity:ExpiresInSeconds")
                );
                _logger.LogInformation("WebApi login. User {User}", registerModel.Email);
                return Ok(new JwtResponseDto()
                {
                    Jwt = jwt,
                    RefreshToken = refreshToken.RefreshToken,
                });
            }
            else
            {
                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new MessageDto("User not found after creation!"));
            }
        }

        var errors = result.Errors.Select(error => error.Description).ToList();
        return BadRequest(new MessageDto("User not found after creation!"));
    }
    
    /// <summary>
    /// Refresh token renewal endpoint for rest API
    /// </summary>
    /// <param name="refreshTokenModel">Data for renewal</param>
    /// <param name="jwtExpiresInSeconds">Custom expiration for jwt</param>
    /// <param name="refreshTokenExpiresInSeconds">Custom expiration for refresh token</param>
    /// <returns></returns>
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
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                return BadRequest(new MessageDto("No token"));
            }
        }
        catch (Exception e)
        {
            return BadRequest(new MessageDto($"Cant parse the token, {e.Message}"));
        }

        // validate jwt, ignore expiration date
        if (!IdentityExtensions.ValidateJwt(
                refreshTokenModel.Jwt,
                _configuration.GetValue<string>("JWTSecurity:Key")!,
                _configuration.GetValue<string>("JWTSecurity:Issuer")!,
                _configuration.GetValue<string>("JWTSecurity:Audience")!
            ))
        {
            return BadRequest("JWT validation fail");
        }

        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest(new MessageDto("No email in jwt"));
        }

        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            return NotFound($"User with email {userEmail} not found");
        }

        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.GivenName, appUser.FirstName));
        await _userManager.AddClaimAsync(appUser, new Claim(ClaimTypes.Surname, appUser.LastName));


        // load and compare refresh tokens

        await _context.Entry(appUser).Collection(u => u.RefreshTokens!)
            .Query()
            .Where(x =>
                (x.RefreshToken == refreshTokenModel.RefreshToken && x.Expiration > DateTime.UtcNow) ||
                (x.PreviousRefreshToken == refreshTokenModel.RefreshToken &&
                 x.PreviousExpiration > DateTime.UtcNow)
            )
            .ToListAsync();

        if (appUser.RefreshTokens == null)
        {
            return Problem("RefreshTokens collection is null");
        }

        if (appUser.RefreshTokens.Count == 0)
        {
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");
        }

        if (appUser.RefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }

        // generate new jwt

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration.GetValue<string>("JWTSecurity:Key")!,
            _configuration.GetValue<string>("JWTSecurity:Issuer")!,
            _configuration.GetValue<string>("JWTSecurity:Audience")!,
            GetExpirationDateTime(jwtExpiresInSeconds, "JWTSecurity:ExpiresInSeconds")
        );

        // make new refresh token, obsolete old ones
        var refreshToken = appUser.RefreshTokens.First();
        if (refreshToken.RefreshToken == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousRefreshToken = refreshToken.RefreshToken;
            refreshToken.PreviousExpiration = DateTime.UtcNow.AddMinutes(1);

            refreshToken.RefreshToken = Guid.NewGuid().ToString();
            refreshToken.Expiration =
                GetExpirationDateTime(refreshTokenExpiresInSeconds, "JWTSecurity:RefreshTokenExpiresInSeconds");

            await _context.SaveChangesAsync();
        }

        var res = new JwtResponseDto()
        {
            Jwt = jwt,
            RefreshToken = refreshToken.RefreshToken,
        };

        return Ok(res);
    }

    private DateTime GetExpirationDateTime(int? expiresInSeconds, string settingsKey)
    {
        if (expiresInSeconds <= 0) expiresInSeconds = int.MaxValue;
        expiresInSeconds = expiresInSeconds < _configuration.GetValue<int>(settingsKey)
            ? expiresInSeconds
            : _configuration.GetValue<int>(settingsKey);

        return DateTime.UtcNow.AddSeconds(expiresInSeconds ?? 60);
    }
}