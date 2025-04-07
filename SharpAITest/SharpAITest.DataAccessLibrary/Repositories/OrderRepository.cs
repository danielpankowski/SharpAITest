using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task DeleteOrder(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderModel> GetOrder(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderModel> InsertOrder(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderModel> UpdateOrder(OrderModel product)
    {
        throw new NotImplementedException();
    }
}
