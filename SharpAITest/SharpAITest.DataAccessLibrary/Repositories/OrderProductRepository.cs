using Dapper;
using SharpAITest.DataAccessLibrary.DTOs;
using SharpAITest.DataAccessLibrary.Extensions;
using SharpAITest.DataAccessLibrary.Mappings;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.DataAccessLibrary.Services.Abstractions;
using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly IAppDbContext dbContext;

    public OrderProductRepository(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderProductModel>> InsertProductSet(IEnumerable<OrderProductModel> orderProduct, int orderId)
    {
        IEnumerable<OrderProductModel> output;
        var dtos = orderProduct.ToDto(orderId);
        var dataTable = dtos.ToCreateDataTable();
        var connection = await dbContext.Connection;
        var result = await connection.QueryAsync<OrderProductDto>(
            "dbo.spOrderProducts_InsertSet",
            new { OrderProducts = dataTable.AsTableValuedParameter("dbo.CreateOrderProductUDT") },
            dbContext.Transaction,
            commandType: System.Data.CommandType.StoredProcedure);
        output = result.ToModel();
        return output;
    }

    public async Task<IEnumerable<OrderProductModel>> GetAllProducts(int orderId)
    {
        IEnumerable<OrderProductModel> output;
        var connection = await dbContext.Connection;
        var result = await connection.QueryAsync<OrderProductDto>(
            "dbo.spOrderProducts_GetByOrderId",
            new { OrderId = orderId },
            dbContext.Transaction,
            commandType: System.Data.CommandType.StoredProcedure);
        output = result?.ToModel();
        return output;
    }

    public async Task DeleteOrderProducts(int orderId)
    {
        var connection = await dbContext.Connection;
        await connection.ExecuteAsync(
            "dbo.spOrderProducts_DeleteByOrderId",
            new { OrderId = orderId },
            dbContext.Transaction,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<OrderProductModel>> UpdateProductSet(IEnumerable<OrderProductModel> orderProducts, int orderId)
    {
        IEnumerable<OrderProductModel> output;
        var dtos = orderProducts.ToDto(orderId);
        var dataTable = dtos.ToUpdateDataTable();
        var connection = await dbContext.Connection;
        var result = await connection.QueryAsync<OrderProductDto>(
            "dbo.spOrderProducts_UpdateSet",
            new { OrderProducts = dataTable.AsTableValuedParameter("dbo.UpdateOrderProductUDT") },
            dbContext.Transaction,
            commandType: System.Data.CommandType.StoredProcedure);
        output = result?.ToModel();
        return output;
    }
}
