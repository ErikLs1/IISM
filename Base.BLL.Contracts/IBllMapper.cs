using Base.Contracts;

namespace Base.BLL.Contracts;



public interface IBllMapper<TBllEntity, TDalEntity> : IBllMapper<TBllEntity, TDalEntity, Guid>
    where TDalEntity : class, IDomainId
    where TBllEntity : class, IDomainId
{
    
}

public interface IBllMapper<TBllEntity, TDalEntity, TKey> 
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainId<TKey>
    where TBllEntity : class, IDomainId<TKey>
{
    public TDalEntity? Map(TBllEntity? entity);
    public TBllEntity? Map(TDalEntity? entity);
}