using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories.Abstractions;

public interface IOrderRepository
{
    Task<OrderModel> InsertOrder(int Id);
    Task<OrderModel> GetOrder(int Id);
    Task<OrderModel> UpdateOrder(OrderModel product);
    Task DeleteOrder(int Id);
}