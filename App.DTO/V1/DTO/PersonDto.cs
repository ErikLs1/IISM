namespace App.DTO.V1.DTO;

public class PersonDto
{
    public Guid Id { get; set; }
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;
}