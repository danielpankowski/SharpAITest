using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<OrderModel> InsertOrder(OrderModel order);
    Task<OrderModel> GetOrder(int id);
    Task<OrderModel> UpdateOrder(OrderModel order);
    Task DeleteOrder(int id);
    Task<IEnumerable<OrderModel>> GetAllOrders();
}