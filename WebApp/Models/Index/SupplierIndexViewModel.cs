using App.BLL.DTO;

namespace WebApp.Models.Index;

public class SupplierIndexViewModel
{
    public ICollection<SupplierBllDto> Suppliers { get; set; } = default!;

}