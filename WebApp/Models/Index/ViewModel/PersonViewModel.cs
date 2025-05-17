using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class PersonViewModel
{
    public ICollection<PersonMvcDto> Persons { get; set; } = default!;
    public int PersonCountByName { get; set; }
}