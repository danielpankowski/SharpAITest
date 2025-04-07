using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.OrderProducts;

public record UpdateOrderProductRequest
(
    [Required]
    int Id,
    [Required]
    int ProductId,
    [Required]
    int Quantity
);
