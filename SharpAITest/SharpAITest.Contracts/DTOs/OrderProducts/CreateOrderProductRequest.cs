using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.OrderProducts;

public record CreateOrderProductRequest
(
    [Required]
    int ProductId,
    [Required]
    int Quantity
);
