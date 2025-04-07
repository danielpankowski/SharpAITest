using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services.Abstraction;

public interface IOrderProductService
{
    Task<IEnumerable<OrderProductModel>> InsertProductSet(IEnumerable<OrderProductModel> orderProduct, int orderId);
    Task<IEnumerable<OrderProductModel>> GetAllProducts(int orderId);
    Task<IEnumerable<OrderProductModel>> UpdateProductSet(IEnumerable<OrderProductModel> orderProducts, int orderId);
    Task DeleteOrderProduct(int id);
}
