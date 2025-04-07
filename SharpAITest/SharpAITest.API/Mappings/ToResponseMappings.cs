using SharpAITest.Contracts.DTOs.OrderProducts;
using SharpAITest.Contracts.DTOs.Orders;
using SharpAITest.Contracts.DTOs.Products;
using SharpAITest.Domain.Models;

namespace SharpAITest.API.Mappings;

public static class ToResponseMappings
{
    public static OrderResponse ToResponse(this OrderModel order)
    {
        return new OrderResponse
        (
            order.Id,
            order.OrderedProducts?.ToResponse(),
            order.TotalPrice
        );
    }

    public static IEnumerable<OrderResponse> ToResponse(this IEnumerable<OrderModel> orders)
    {
        return orders.Select(o => o.ToResponse());
    }

    public static OrderProductResponse ToResponse(this OrderProductModel orderProduct)
    {
        return new OrderProductResponse
        (
            orderProduct.Id,
            orderProduct.Product?.ToResponse(),
            orderProduct.Quantity
        );
    }

    public static IEnumerable<OrderProductResponse> ToResponse(this IEnumerable<OrderProductModel> orderProducts)
    {
        return orderProducts.Select(p => p.ToResponse());
    }

    public static ProductResponse ToResponse(this ProductModel product)
    {
        return new ProductResponse
        (
            product.Id,
            product.Name,
            product.Price
        );
    }

    public static IEnumerable<ProductResponse> ToResponse(this IEnumerable<ProductModel> products)
    {
        return products.Select(p => p.ToResponse());
    }
}
