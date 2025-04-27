using App.Domain;

namespace WebApp.Models;

public class PersonIndexViewModel
{
    public ICollection<Person> Persons { get; set; } = default!;
    public int PersonCountByName { get; set; }
}