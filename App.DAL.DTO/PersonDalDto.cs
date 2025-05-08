using Base.Contracts;

namespace App.DAL.DTO;

public class PersonDalDto : IDomainId
{
    public Guid Id { get; set; } 
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;
    public string PersonPhoneNumber { get; set; } = default!;
    public string PersonAddress { get; set; } = default!;
    public string PersonGender { get; set; } = default!;
    public DateOnly? PersonDateOfBirth { get; set; } 
    public ICollection<OrderDalDto>? Orders { get; set; } = new List<OrderDalDto>();
}