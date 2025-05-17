namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class RefundMvcDto
{
    public Guid Id { get; set; }
    public decimal RefundAmount { get; set; } 
    public string RefundReason { get; set; } = default!;
    public string RefundStatus { get; set; } = default!;
}