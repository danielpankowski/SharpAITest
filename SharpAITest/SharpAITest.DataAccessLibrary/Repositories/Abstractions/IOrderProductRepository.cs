using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories.Abstractions;

public interface IOrderProductRepository
{
    Task<IEnumerable<OrderProductModel>> InsertProductSet(IEnumerable<OrderProductModel> orderProducts, int orderId);
    Task<IEnumerable<OrderProductModel>> GetAllProducts(int orderId);
    Task<IEnumerable<OrderProductModel>> UpdateProductSet(IEnumerable<OrderProductModel> orderProducts, int orderId);
    Task DeleteOrderProducts(int orderId);
}
