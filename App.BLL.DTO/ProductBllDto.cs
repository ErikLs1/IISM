using Base.Contracts;

namespace App.BLL.DTO;

public class ProductBllDto : IDomainId
{
    public Guid Id { get; set; }
}