using Base.Domain.Identity;

namespace App.Domain.Identity;

// TODO later: Check if this id is necessary
public class AppUserRole : BaseUserRole<AppUser, AppRole>
{
    public new Guid Id { get; set; } = Guid.NewGuid();
}