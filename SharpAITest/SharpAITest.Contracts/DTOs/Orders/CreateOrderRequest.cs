using SharpAITest.Contracts.DTOs.OrderProducts;
using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.Orders;

public record CreateOrderRequest
(
    [Required]
    IEnumerable<CreateOrderProductRequest> Products,
    [Required]
    decimal TotalPrice
);