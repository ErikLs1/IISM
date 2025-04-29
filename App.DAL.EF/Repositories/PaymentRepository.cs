using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PaymentRepository : BaseRepository<PaymentDto, Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PaymentMapper())
    {
    }
}