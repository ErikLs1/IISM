namespace WebApp.Models.Index.MvcDto;

/// <summary>
/// 
/// </summary>
public class PersonMvcDto
{
    public Guid Id { get; set; }
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;
    public string PersonPhoneNumber { get; set; } = default!;
    public string PersonAddress { get; set; } = default!;
    public string PersonGender { get; set; } = default!;
    public DateOnly? PersonDateOfBirth { get; set; }
}