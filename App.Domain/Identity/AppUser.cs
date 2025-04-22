using Base.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    public Person? Person { get; set; }
}