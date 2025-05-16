using Microsoft.AspNetCore.Mvc;
using App.DAL.EF;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    /// <inheritdoc />
    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    // TODO - MAYBE LATER ADD OPTION FOR OWNERS TO ADD OWN CREATED PRODUCTS TO WAREHOUSE
}