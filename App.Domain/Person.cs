using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntityUser<AppUser, AppRole>
{
    [Required]
    [MaxLength(50)]
    [Display(Name = nameof(PersonFirstName), Prompt = nameof(PersonFirstName), ResourceType = typeof(App.Resources.Domain.Person))]
    public string PersonFirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    [Display(Name = nameof(PersonLastName), Prompt = nameof(PersonLastName), ResourceType = typeof(App.Resources.Domain.Person))]
    public string PersonLastName { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(PersonPhoneNumber), Prompt = nameof(PersonPhoneNumber), ResourceType = typeof(App.Resources.Domain.Person))]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [MaxLength(200)]
    [Display(Name = nameof(PersonAddress), Prompt = nameof(PersonAddress), ResourceType = typeof(App.Resources.Domain.Person))]
    public string PersonAddress { get; set; } = default!;
    
    [MaxLength(20)]
    [Display(Name = nameof(PersonGender), Prompt = nameof(PersonGender), ResourceType = typeof(App.Resources.Domain.Person))]
    public string PersonGender { get; set; } = default!;
    
    [Display(Name = nameof(PersonDateOfBirth), Prompt = nameof(PersonDateOfBirth), ResourceType = typeof(App.Resources.Domain.Person))]
    public DateTime? PersonDateOfBirth { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}