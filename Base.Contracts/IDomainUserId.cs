using Base.Domain.Identity;

namespace Base.Contracts;

public interface IDomainUserId : IDomainUserId<Guid, BaseUser>
{
}

public interface IDomainUserId<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : BaseUser<TKey>
{
    TKey UserId { get; set; }
    TUser User { get; set; }
}