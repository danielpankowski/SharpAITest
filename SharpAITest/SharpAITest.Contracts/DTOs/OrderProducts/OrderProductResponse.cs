using SharpAITest.Contracts.DTOs.Products;

namespace SharpAITest.Contracts.DTOs.OrderProducts;

public record OrderProductResponse
(
    int Id,
    ProductResponse Product,
    int Quantity
);
