using App.DAL.Contracts;
using App.DAL.DTO;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PaymentRepository : BaseRepository<PaymentDalDto, Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext repositoryDbContext, IUowMapper<PaymentDalDto, Payment> uowMapper) : base(repositoryDbContext, uowMapper)
    {
    }
}