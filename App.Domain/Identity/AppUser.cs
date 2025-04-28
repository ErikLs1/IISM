using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    public ICollection<Person>? Persons { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
}