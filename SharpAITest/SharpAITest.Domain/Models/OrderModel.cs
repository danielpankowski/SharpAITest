namespace SharpAITest.Domain.Models;

public class OrderModel
{
    public int Id { get; set; }
    public IEnumerable<OrderProductModel> OrderedProducts { get; set; }
    public decimal TotalPrice { get; set; }

    public bool Equals(OrderModel? other)
    {
        if (other is null)
            return false;

        return
            TotalPrice == other.TotalPrice;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OrderModel);
    }
}
