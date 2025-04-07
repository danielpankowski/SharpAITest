namespace SharpAITest.Domain.Models;

public class OrderModel
{
    public int Id { get; set; }
    public IEnumerable<OrderProductModel> OrderedProducts { get; set; }
    public decimal FullPrice { get; set; }
}
