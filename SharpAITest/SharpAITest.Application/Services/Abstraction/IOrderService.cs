using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services.Abstraction;

public interface IOrderService
{
    Task<OrderModel> InsertOrder(OrderModel order);
    Task<OrderModel> GetFullOrder(int id);
    Task<OrderModel> UpdateOrder(OrderModel order);
    Task DeleteOrder(int Id);
}
