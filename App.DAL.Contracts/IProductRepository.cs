using App.DAL.DTO;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IProductRepository : IBaseRepository<ProductDalDto>
{
    Task<Decimal> GetProductPriceById(Guid productId);
}