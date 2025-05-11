using Base.Contracts;

namespace App.DTO.V1.DTO;

public class CategoryDto : IDomainId
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string CategoryDescription { get; set; } = default!;
}