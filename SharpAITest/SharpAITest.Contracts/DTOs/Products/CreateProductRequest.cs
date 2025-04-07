using System.ComponentModel.DataAnnotations;

namespace SharpAITest.Contracts.DTOs.Products;

public record CreateProductRequest
(
    [Required]
    [StringLength(50)]
    string Name,
    [Required]
    decimal Price
);
