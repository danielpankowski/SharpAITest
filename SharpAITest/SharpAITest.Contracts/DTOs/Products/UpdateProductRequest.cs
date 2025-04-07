using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.Products;

public record UpdateProductRequest
(
    [Required]
    int Id,
    [Required]
    [StringLength(50)]
    string Name,
    [Required]
    decimal Price
);
