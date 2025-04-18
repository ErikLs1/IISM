using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Category : BaseEntity
{
    [MaxLength(50)]
    [Display(Name = nameof(CategoryName), Prompt = nameof(CategoryName), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryName { get; set; } = default!;
    
    [MaxLength(250)]
    [Display(Name = nameof(CategoryDescription), Prompt = nameof(CategoryDescription), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryDescription { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Product> Products { get; set; } = new List<Product>();

}