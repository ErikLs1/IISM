using Base.Contracts;

namespace App.DAL.DTO;

public class CategoryDalDto : IDomainId
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public string CategoryDescription { get; set; } = default!;
    public ICollection<ProductDalDto>? Products { get; set; } = new List<ProductDalDto>();
}