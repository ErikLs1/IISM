using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntity
{
    [MaxLength(50)]
    public string CategoryName { get; set; } = default!;
    
    [MaxLength(250)]
    public string CategoryDescription { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();

}