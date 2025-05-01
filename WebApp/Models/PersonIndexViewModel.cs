using App.DAL.DTO;

namespace WebApp.Models;

public class PersonIndexViewModel
{
    public ICollection<PersonDalDto> Persons { get; set; } = default!;
    public int PersonCountByName { get; set; }
}