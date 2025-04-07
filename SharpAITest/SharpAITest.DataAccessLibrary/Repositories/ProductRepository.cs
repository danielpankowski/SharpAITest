using Dapper;
using SharpAITest.DataAccessLibrary.DTOs;
using SharpAITest.DataAccessLibrary.Mappings;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.DataAccessLibrary.Services.Abstractions;
using SharpAITest.Domain.Models;
using System.Data;

namespace SharpAITest.DataAccessLibrary.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly IAppDbContext dbContext;

    public ProductRepository(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ProductModel> InsertProduct(ProductModel product)
    {
        ProductModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<ProductDto>(
            "dbo.spProducts_Insert",
            new 
            { 
                product.Name,
                product.Price
            },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).First();
        output = result.ToModel();
        return output;
    }

    public async Task<ProductModel> GetProduct(int id)
    {
        ProductModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<ProductDto>(
            "dbo.spProducts_GetById",
            new { Id = id },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).FirstOrDefault();
        output = result.ToModel();
        return output;
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        ProductModel output;
        var connection = await dbContext.Connection;
        var result = (await connection.QueryAsync<ProductDto>(
            "dbo.spProducts_Update",
            new
            {
                product.Id,
                product.Name,
                product.Price
            },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure)).First();
        output = result.ToModel();
        return output;
    }

    public async Task DeleteProduct(int id)
    {
        var connection = await dbContext.Connection;
        var result = await connection.ExecuteAsync(
            "dbo.spProducts_Delete",
            new { Id = id, },
            dbContext.Transaction,
            commandType: CommandType.StoredProcedure);
    }
}
