using SharpAITest.Application.Services.Abstraction;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.Domain.Exceptions;
using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ProductModel> InsertProduct(ProductModel product)
    {
        return await productRepository.InsertProduct(product);
    }

    public async Task<ProductModel> GetProduct(int id)
    {
        ProductModel output;
        output = await productRepository.GetProduct(id);
        if (output is null)
        {
            throw new NotFoundException($"Product with ID {id} not found");
        }

        return output;
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        ProductModel output;
        var retrievedProduct = await productRepository.GetProduct(product.Id);
        if (retrievedProduct is null)
        {
            throw new NotFoundException($"Product with ID {product.Id} not found");
        }
        output = await productRepository.UpdateProduct(product);
        return output;
    }

    public async Task DeleteProduct(int id)
    {
        var retrievedProduct = await productRepository.GetProduct(id);
        if (retrievedProduct is null)
        {
            throw new NotFoundException($"Product with ID {id} not found");
        }
        await productRepository.DeleteProduct(id);
    }
}
