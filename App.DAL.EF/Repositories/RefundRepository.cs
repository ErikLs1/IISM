using App.DAL.Contracts;
using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class RefundRepository : BaseRepository<RefundDalDto, Refund>, IRefundRepository
{
    public RefundRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RefundUowMapper())
    {
    }
}