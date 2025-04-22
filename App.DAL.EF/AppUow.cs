using App.DAL.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUow : BaseUow<AppDbContext>, IAppUow
{
    public AppUow(AppDbContext context) : base(context)
    {
    }
}