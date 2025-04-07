using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories.Abstractions;

public interface IProductRepository
{
    Task<ProductModel> InsertProduct(int Id);
    Task<ProductModel> GetProduct(int Id);
    Task<ProductModel> UpdateProduct(ProductModel product);
    Task DeleteProduct(int Id);
}
