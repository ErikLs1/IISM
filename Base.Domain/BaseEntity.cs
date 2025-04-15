using System.ComponentModel.DataAnnotations;

namespace Base.Domain;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

}