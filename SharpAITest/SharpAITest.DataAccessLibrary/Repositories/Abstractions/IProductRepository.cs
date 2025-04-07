using SharpAITest.Domain.Models;

namespace SharpAITest.DataAccessLibrary.Repositories.Abstractions;

public interface IProductRepository
{
    Task<ProductModel> InsertProduct(ProductModel product);
    Task<ProductModel> GetProduct(int id);
    Task<ProductModel> UpdateProduct(ProductModel product);
    Task DeleteProduct(int id);
    Task<IEnumerable<ProductModel>> GetAllProducts();
}
