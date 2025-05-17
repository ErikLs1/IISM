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
    public string WarehouseAddress { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehouseStreet { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehouseCity { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehouseState { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehouseCountry { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehousePostalCode { get; set; } = default!;
    
    
    /// <summary>
    /// 
    /// </summary>
    public string WarehouseEmail { get; set; } = default!;
    
    /// <summary>
    /// 
    /// </summary>
    public int WarehouseCapacity { get; set; }
}