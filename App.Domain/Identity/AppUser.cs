using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    /*
    public Person? Person { get; set; }
    */
    public ICollection<Person>? Persons { get; set; }

}