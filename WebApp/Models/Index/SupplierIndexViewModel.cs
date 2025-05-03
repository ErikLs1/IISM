using App.DAL.DTO;

namespace WebApp.Models.Index;

public class SupplierIndexViewModel
{
    public ICollection<SupplierDalDto> Suppliers { get; set; } = default!;

}