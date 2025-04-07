using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories;

internal class ProductRepository : IProductRepository
{
    public Task DeleteProduct(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> GetProduct(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> InsertProduct(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductModel> UpdateProduct(ProductModel product)
    {
        throw new NotImplementedException();
    }
}
