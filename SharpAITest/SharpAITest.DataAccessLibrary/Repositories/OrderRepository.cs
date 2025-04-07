using Dapper;
using SharpAITest.DataAccessLibrary.DTOs;
using SharpAITest.DataAccessLibrary.Mappings;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.DataAccessLibrary.Services.Abstractions;
using SharpAITest.Domain.Models;
using System.Data;

namespace SharpAITest.DataAccessLibrary.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IAppDbContext dbContext;

    public OrderRepository(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<OrderModel> InsertOrder(OrderModel order)
    {
        OrderModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<OrderDto>(
            "dbo.spOrders_Insert",
            new { order.TotalPrice },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).First();
        output = result.ToModel();
        return output;
    }

    public async Task<IEnumerable<OrderModel>> GetAllOrders()
    {
        IEnumerable<OrderModel> output;
        var connection = await dbContext.Connection;
        var result = await connection.QueryAsync<OrderDto>(
            "dbo.spOrders_GetAll",
            null,
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure);
        output = result.Select(x => x.ToModel());
        return output;
    }

    public async Task<OrderModel> GetOrder(int id)
    {
        OrderModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<OrderDto>(
            "dbo.spOrders_GetById",
            new { Id = id },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).FirstOrDefault();
        output = result?.ToModel();
        return output;
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order)
    {
        OrderModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<OrderDto>(
            "dbo.spOrders_Update",
            new
            {
                order.Id,
                order.TotalPrice
            },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).First();
        output = result.ToModel();
        return output;
    }

    public async Task DeleteOrder(int id)
    {
        var connection = await dbContext.Connection;
        var result = await connection.ExecuteAsync(
            "dbo.spOrders_Delete",
            new { Id = id, },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure);
    }
}
