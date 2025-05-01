using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class CategoryBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    [Display(Name = nameof(CategoryName), Prompt = nameof(CategoryName), ResourceType = typeof(Category))]
    public string CategoryName { get; set; } = default!;
    
    [MaxLength(250)]
    [Display(Name = nameof(CategoryDescription), Prompt = nameof(CategoryDescription), ResourceType = typeof(Category))]
    public string CategoryDescription { get; set; } = default!;
    
    public ICollection<ProductBllDto>? Products { get; set; } = new List<ProductBllDto>();
}