using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class PersonViewModel
{
    public ICollection<PersonBllDto> Persons { get; set; } = default!;
    public int PersonCountByName { get; set; }
}