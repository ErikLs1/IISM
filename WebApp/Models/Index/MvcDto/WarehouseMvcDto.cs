using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;
/// <summary>
/// 
/// </summary>
public class WarehouseMvcDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseAddress), ResourceType = typeof(Warehouse))]
    public string WarehouseAddress { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseStreet), ResourceType = typeof(Warehouse))]
    public string WarehouseStreet { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseCity), ResourceType = typeof(Warehouse))]
    public string WarehouseCity { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseState), ResourceType = typeof(Warehouse))]
    public string WarehouseState { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseCountry), ResourceType = typeof(Warehouse))]
    public string WarehouseCountry { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehousePostalCode), ResourceType = typeof(Warehouse))]
    public string WarehousePostalCode { get; set; } = default!;
    
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseEmail), ResourceType = typeof(Warehouse))]
    public string WarehouseEmail { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    [Display(Name = nameof(Warehouse.WarehouseCapacity), ResourceType = typeof(Warehouse))]
    public int WarehouseCapacity { get; set; }
}