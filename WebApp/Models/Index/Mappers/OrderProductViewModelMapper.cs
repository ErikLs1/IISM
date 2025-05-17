using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class OrderProductViewModelMapper
{
    public OrderProductMvcDto Map(OrderProductBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new OrderProductMvcDto()
        {
        };
    }
}