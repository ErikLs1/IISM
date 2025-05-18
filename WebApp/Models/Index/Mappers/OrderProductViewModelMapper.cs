using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class OrderProductViewModelMapper
{
    public OrderProductMvcDto Map(OrderProductBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new OrderProductMvcDto
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            ProductName = dto.Product!.ProductName
        };
    }
    
    /*public OrderProductBllDto Map(OrderProductMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new OrderProductBllDto
        {
            Id = dto.Id,
            Quantity = dto.Quantity,
            TotalPrice = dto.TotalPrice,
            ProductName = dto.Product!.ProductName
        };
    }*/
}