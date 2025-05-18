using App.BLL.DTO;
using WebApp.Models.Index.MvcDto;

namespace WebApp.Models.Index.Mappers;

public class RefundViewModelMapper
{
    public RefundMvcDto Map(RefundBllDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new RefundMvcDto()
        {
            Id = dto.Id,
            OrderProductId = dto.OrderProductId,
            RefundAmount = dto.RefundAmount,
            RefundReason = dto.RefundReason,
            RefundStatus = dto.RefundStatus
        };
    }
    
    public RefundBllDto Map(RefundMvcDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));
        
        return new RefundBllDto()
        {
            Id = dto.Id,
            OrderProductId = dto.OrderProductId,
            RefundAmount = dto.RefundAmount,
            RefundReason = dto.RefundReason,
            RefundStatus = dto.RefundStatus
        };
    }
}