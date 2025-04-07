using SharpAITest.Contracts.DTOs.OrderProducts;
using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.Orders;

public record UpdateOrderRequest
(
    [Required]
    int Id,
    [Required]
    IEnumerable<UpdateOrderProductRequest> Products,
    [Required]
    decimal TotalPrice

);
