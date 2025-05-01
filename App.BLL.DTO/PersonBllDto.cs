using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Contracts;

namespace App.BLL.DTO;

public class PersonBllDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Display(Name = nameof(PersonFirstName), Prompt = nameof(PersonFirstName), ResourceType = typeof(Person))]
    public string PersonFirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    [Display(Name = nameof(PersonLastName), Prompt = nameof(PersonLastName), ResourceType = typeof(Person))]
    public string PersonLastName { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(PersonPhoneNumber), Prompt = nameof(PersonPhoneNumber), ResourceType = typeof(Person))]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [MaxLength(200)]
    [Display(Name = nameof(PersonAddress), Prompt = nameof(PersonAddress), ResourceType = typeof(Person))]
    public string PersonAddress { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(PersonGender), Prompt = nameof(PersonGender), ResourceType = typeof(Person))]
    public string PersonGender { get; set; } = default!;
    
    [Display(Name = nameof(PersonDateOfBirth), Prompt = nameof(PersonDateOfBirth), ResourceType = typeof(Person))]
    public DateTime? PersonDateOfBirth { get; set; } = DateTime.UtcNow;

    public ICollection<OrderBllDto>? Orders { get; set; } = new List<OrderBllDto>();
}