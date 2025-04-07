using SharpAITest.DataAccessLibrary.DTOs;
using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Mappings;

public static class ToModelMappings
{
    public static ProductModel ToModel(this ProductDto dto)
    {
        return new ProductModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Price = dto.Price
        };
    }

    public static OrderProductModel ToModel(this OrderProductDto dto)
    {
        return new OrderProductModel
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
    }

    public static IEnumerable<OrderProductModel> ToModel(this IEnumerable<OrderProductDto> dtos)
    {
        return dtos.Select(dto => dto.ToModel());
    }

    public static OrderModel ToModel(this OrderDto dto)
    {
        return new OrderModel
        {
            Id = dto.Id,
            TotalPrice = dto.TotalPrice
        };
    }
}
