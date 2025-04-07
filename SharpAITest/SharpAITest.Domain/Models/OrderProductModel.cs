namespace SharpAITest.Domain.Models;

public class OrderProductModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    public int Quantity { get; set; }
}