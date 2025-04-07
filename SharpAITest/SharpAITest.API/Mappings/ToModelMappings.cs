using SharpAITest.Contracts.DTOs.OrderProducts;
using SharpAITest.Contracts.DTOs.Orders;
using SharpAITest.Contracts.DTOs.Products;
using SharpAITest.Domain.Models;

namespace SharpAITest.API.Mappings;

public static class ToModelMappings
{
    public static OrderModel ToModel(this CreateOrderRequest request)
    {
        return new OrderModel
        {
            TotalPrice = request.TotalPrice,
            OrderedProducts = request.Products.ToModel().ToList()
        };
    }

    public static OrderProductModel ToModel(this CreateOrderProductRequest request)
    {
        return new OrderProductModel
        {
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }

    public static IEnumerable<OrderProductModel> ToModel(this IEnumerable<CreateOrderProductRequest> request)
    {
        return request.Select(p => p.ToModel());
    }

    public static ProductModel ToModel(this CreateProductRequest request)
    {
        return new ProductModel
        {
            Name = request.Name,
            Price = request.Price
        };
    }

    public static OrderModel ToModel(this UpdateOrderRequest request)
    {
        return new OrderModel
        {
            Id = request.Id,
            TotalPrice = request.TotalPrice,
            OrderedProducts = request.Products.ToModel().ToList()
        };
    }

    public static ProductModel ToModel(this UpdateProductRequest request)
    {
        return new ProductModel
        {
            Id = request.Id,
            Name = request.Name,
            Price = request.Price
        };
    }

    public static OrderProductModel ToModel(this UpdateOrderProductRequest request)
    {
        return new OrderProductModel
        {
            Id = request.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }

    public static IEnumerable<OrderProductModel> ToModel(this IEnumerable<UpdateOrderProductRequest> request)
    {
        return request.Select(p => p.ToModel());
    }
}
