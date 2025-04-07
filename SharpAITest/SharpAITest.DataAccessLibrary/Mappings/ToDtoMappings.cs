using SharpAITest.DataAccessLibrary.DTOs;
using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Mappings;

public static class ToDtoMappings
{
    public static OrderProductDto ToDto(this OrderProductModel model, int orderId)
    {
        return new OrderProductDto
        (
            model.Id,
            orderId,
            model.ProductId,
            model.Quantity
        );
    }

    public static IEnumerable<OrderProductDto> ToDto(this IEnumerable<OrderProductModel> models, int orderId)
    {
        return models.Select(model => model.ToDto(orderId));
    }
}
