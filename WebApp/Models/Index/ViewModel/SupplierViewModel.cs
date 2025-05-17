using App.BLL.DTO;

namespace WebApp.Models.Index.ViewModel;

/// <summary>
/// 
/// </summary>
public class SupplierViewModel
{
    public ICollection<SupplierBllDto> Suppliers { get; set; } = default!;

}