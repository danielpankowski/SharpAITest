using SharpAITest.Application.Services.Abstraction;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.Domain.Exceptions;
using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services;

public class OrderProductService : IOrderProductService
{
    private readonly IOrderProductRepository orderProductRepository;

    public OrderProductService(IOrderProductRepository orderProductRepository)
    {
        this.orderProductRepository = orderProductRepository;
    }

    public async Task<IEnumerable<OrderProductModel>> InsertProductSet(IEnumerable<OrderProductModel> orderProduct, int orderId)
    {
        return await orderProductRepository.InsertProductSet(orderProduct, orderId);
    }

    public async Task<IEnumerable<OrderProductModel>> GetAllProducts(int orderId)
    {
        var output = await orderProductRepository.GetAllProducts(orderId);
        if(output is null)
        {
            throw new NotFoundException($"Product orders for order with ID {orderId} not found");
        }
        return output;
    }

    public async Task DeleteOrderProduct(int orderId)
    {
        await orderProductRepository.DeleteOrderProducts(orderId);
    }

    public async Task<IEnumerable<OrderProductModel>> UpdateProductSet(IEnumerable<OrderProductModel> orderProducts, int orderId)
    {
        return await orderProductRepository.UpdateProductSet(orderProducts, orderId);
    }
}
