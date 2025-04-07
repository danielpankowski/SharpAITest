using SharpAITest.Contracts.DTOs.OrderProducts;

namespace SharpAITest.Contracts.DTOs.Orders;

public record OrderResponse
(
    int Id,
    IEnumerable<OrderProductResponse> Products,
    decimal TotalPrice
);
