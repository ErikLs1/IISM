namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class InventoryMvcDto
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? ProductName { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? ProductDescription { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string? WarehouseAddress { get; set; }
}