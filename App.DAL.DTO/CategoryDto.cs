using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.DAL.DTO;

public class CategoryDto : IDomainId
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    [Display(Name = nameof(CategoryName), Prompt = nameof(CategoryName), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryName { get; set; } = default!;
    
    [MaxLength(250)]
    [Display(Name = nameof(CategoryDescription), Prompt = nameof(CategoryDescription), ResourceType = typeof(App.Resources.Domain.Category))]
    public string CategoryDescription { get; set; } = default!;
    
    public ICollection<ProductDto>? Products { get; set; } = new List<ProductDto>();
}