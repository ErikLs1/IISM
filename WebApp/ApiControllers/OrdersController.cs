using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.DTO.V1.DTO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers;

/// <inheritdoc />
[ApiVersion( "1.0" )]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrdersController : ControllerBase
{
    private readonly IAppBll _bll;

    /// <inheritdoc />
    public OrdersController(IAppBll bll)
    {
        _bll = bll;
    }

    // TODO - ORDER PLACING
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), 201)]
    public async Task<IActionResult> PlaceTheOrder(CreateOrderDto dto)
    {
        throw new NotImplementedException();
    }
    
    // TODO - ENDPOINT FOR USERS ORDERS
    [HttpGet]
    [ProducesResponseType(typeof(OrderDto), 200)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetUsersOrders()
    {
        throw new NotImplementedException();
    }
    
    // TODO - ENDPOINT FOR ALL ORDER (FOR MANAGER)
    [HttpGet]
    [Authorize(Roles = "manager")]
    [ProducesResponseType(typeof(OrderDto), 200)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrder()
    {
        throw new NotImplementedException();
    }
}