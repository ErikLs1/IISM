using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class BaseUser : BaseUser<Guid>
{
    
}

public class BaseUser<TKey> : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
{
    
}