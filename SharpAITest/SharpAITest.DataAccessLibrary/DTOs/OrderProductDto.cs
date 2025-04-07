namespace SharpAITest.DataAccessLibrary.DTOs;

public record OrderProductDto
(
    int Id,
    int OrderId,
    int ProductId,
    int Quantity
);