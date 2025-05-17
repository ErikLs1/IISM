using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

/// <summary>
/// 
/// </summary>
public class PaymentViewModelMapper
{
    public PaymentMvcDto Map(PaymentBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new PaymentMvcDto()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            PaymentMethod = dto.PaymentMethod,
            PaymentStatus = dto.PaymentStatus,
            PaymentAmount = dto.PaymentAmount,
            PaymentDate = dto.PaymentDate,
        };
    }
}