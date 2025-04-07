using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services.Abstraction;

public interface IProductService
{
    Task<ProductModel> InsertProduct(ProductModel product);
    Task<ProductModel> GetProduct(int id);
    Task<ProductModel> UpdateProduct(ProductModel product);
    Task DeleteProduct(int id);
}
