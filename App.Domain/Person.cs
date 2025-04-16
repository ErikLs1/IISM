using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string PersonFirstName { get; set; } = default!;
    
    [Required]
    [MaxLength(50)]
    public string PersonLastName { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonPhoneNumber { get; set; } = default!;
    
    [MaxLength(200)]
    public string PersonAddress { get; set; } = default!;
    
    [MaxLength(20)]
    public string PersonGender { get; set; } = default!;
    
    public DateTime? PersonDateOfBirth { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

}