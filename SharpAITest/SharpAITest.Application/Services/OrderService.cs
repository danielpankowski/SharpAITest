using SharpAITest.Application.Services.Abstraction;
using SharpAITest.DataAccessLibrary.Repositories.Abstractions;
using SharpAITest.DataAccessLibrary.Services.Abstractions;
using SharpAITest.Domain.Exceptions;
using SharpAITest.Domain.Models;

namespace SharpAITest.Application.Services;

public class OrderService : IOrderService
{
    private readonly IAppDbContext dbContext;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderProductService orderProductService;

    public OrderService(IAppDbContext dbContext, IOrderRepository orderRepository, IOrderProductService orderProductService)
    {
        this.dbContext = dbContext;
        this.orderRepository = orderRepository;
        this.orderProductService = orderProductService;
    }

    public async Task<OrderModel> InsertOrder(OrderModel order)
    {
        OrderModel output;
        OrderModel insertedOrder;
        IEnumerable<OrderProductModel> insertedOrderProducts;
        await dbContext.BeginTransactionAsync();
        try
        {
            insertedOrder = await orderRepository.InsertOrder(order);
            insertedOrderProducts = await orderProductService.InsertProductSet(order.OrderedProducts, insertedOrder.Id);
            output = insertedOrder;
            output.OrderedProducts = insertedOrderProducts;

            await dbContext.CommitAsync();
            return output;
        }
        catch
        {
            await dbContext.RollbackAsync();
            throw;
        }
    }

    public async Task<OrderModel> GetFullOrder(int id)
    {
        OrderModel output = await orderRepository.GetOrder(id);
        output.OrderedProducts = await orderProductService.GetAllProducts(id);
        return output;
    }

    public async Task<OrderModel> UpdateOrder(OrderModel updatedOrder)
    {
        await dbContext.BeginTransactionAsync();
        try
        {
            OrderModel retrievedUpdatedOrder;
            var currentOrder = await GetFullOrder(updatedOrder.Id);
            await HandleProductChanges(currentOrder.OrderedProducts, updatedOrder.OrderedProducts, updatedOrder.Id);
            if (Equals(updatedOrder, currentOrder) == false)
            {
                retrievedUpdatedOrder = await orderRepository.UpdateOrder(updatedOrder);
            }
            await dbContext.CommitAsync();
        }
        catch
        {
            await dbContext.RollbackAsync();
            throw;
        }

    }

    public async Task DeleteOrder(int Id)
    {
        var retrievedOrder = await orderRepository.GetOrder(Id);
        if (retrievedOrder is null)
        {
            throw new NotFoundException($"Order with ID {Id} not found");
        }
        await orderProductService.DeleteOrderProduct(Id);
        await orderRepository.DeleteOrder(Id);
    }

    private async Task<IEnumerable<OrderProductModel>> HandleProductChanges(IEnumerable<OrderProductModel> currentProducts, IEnumerable<OrderProductModel> updatedProducts, int orderId)
    {
        List<OrderProductModel> output = new();
        var currentDict = currentProducts.ToDictionary(p => p.ProductId);
        var updatedDict = updatedProducts.ToDictionary(p => p.ProductId);

        var productsToRemove = currentDict.Keys.Except(updatedDict.Keys);
        foreach (var productId in productsToRemove)
        {
            await orderProductService.DeleteOrderProduct(productId);
        }

        var productsToInsert = updatedDict.Keys.Except(currentDict.Keys).Select(key => updatedDict[key]);
        if (productsToInsert.Count() > 0)
        {
            await orderProductService.InsertProductSet(productsToInsert, orderId);
        }

        var productsToUpdate = updatedDict.Keys.Intersect(currentDict.Keys)
                                 .Select(k => new
                                 {
                                     Current = currentDict[k],
                                     Updated = updatedDict[k]
                                 })
                                 .Where(p => HasProductChanged(p.Current, p.Updated))
                                 .ToList();

        var retrievedUpdatedProducts = await orderProductService.UpdateOrderProduct(orderId, products);
    }

    private bool HasProductChanged(OrderProductModel currect, OrderProductModel updated)
    {
        return currect.Quantity != updated.Quantity ||
               currect.Product.Id != updated.Product.Id ||
               currect.Product != updated.Product;
    }
}
