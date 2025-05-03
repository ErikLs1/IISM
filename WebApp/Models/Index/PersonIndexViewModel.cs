using App.BLL.DTO;

namespace WebApp.Models.Index;

public class PersonIndexViewModel
{
    public ICollection<PersonBllDto> Persons { get; set; } = default!;
    public int PersonCountByName { get; set; }
}