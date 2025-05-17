using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

public class CategoryMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Category.CategoryName), ResourceType = typeof(Category))]
    public string CategoryName { get; set; } = default!;
    
    [Display(Name = nameof(Category.CategoryDescription), ResourceType = typeof(Category))]
    public string CategoryDescription { get; set; } = default!;
}