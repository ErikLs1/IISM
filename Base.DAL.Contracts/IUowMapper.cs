using Base.Contracts;

namespace Base.DAL.Contracts;

public interface IUowMapper<TDalEntity, TDomainEntity> : IUowMapper<TDalEntity, TDomainEntity, Guid>
    where TDalEntity : class, IDomainId
    where TDomainEntity : class, IDomainId
{
    
}

public interface IUowMapper<TDalEntity, TDomainEntity, TKey> 
    where TKey : IEquatable<TKey>
    where TDalEntity : class, IDomainId<TKey>
    where TDomainEntity : class, IDomainId<TKey>
{
    public TDalEntity? Map(TDomainEntity? entity);
    public TDomainEntity? Map(TDalEntity? entity);
}