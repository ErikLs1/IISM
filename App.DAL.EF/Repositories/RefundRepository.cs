using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RefundRepository : BaseRepository<Refund>, IRefundRepository
{
    public RefundRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}