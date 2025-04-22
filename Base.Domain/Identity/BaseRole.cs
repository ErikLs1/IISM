using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class BaseRole : BaseRole<Guid>
{
    
}

public class BaseRole<TKey> : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    
}