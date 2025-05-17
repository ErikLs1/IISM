using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;

namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class PersonMvcDto
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Person.PersonFirstName), ResourceType = typeof(Person))]
    public string PersonFirstName { get; set; } = default!;
    
    [Display(Name = nameof(Person.PersonLastName), ResourceType = typeof(Person))]
    public string PersonLastName { get; set; } = default!;
    
    [Display(Name = nameof(Person.PersonPhoneNumber), ResourceType = typeof(Person))]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [Display(Name = nameof(Person.PersonAddress), ResourceType = typeof(Person))]
    public string PersonAddress { get; set; } = default!;
    
    [Display(Name = nameof(Person.PersonGender), ResourceType = typeof(Person))]
    public string PersonGender { get; set; } = default!;
    
    [Display(Name = nameof(Person.PersonDateOfBirth), ResourceType = typeof(Person))]
    public DateOnly? PersonDateOfBirth { get; set; }
}