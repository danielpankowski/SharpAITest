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

    public Task<IEnumerable<OrderModel>> GetAllOrders()
    {
        return orderRepository.GetAllOrders();
    }

    public async Task<OrderModel> GetFullOrder(int id)
    {
        OrderModel output = await orderRepository.GetOrder(id);
        if(output is null)
        {
            throw new NotFoundException($"Order with ID {id} not found");
        }
        output.OrderedProducts = (await orderProductService.GetAllProducts(id));
        return output;
    }

    public async Task<OrderModel> UpdateOrder(OrderModel updatedOrder)
    {
        OrderModel output;
        await dbContext.BeginTransactionAsync();
        try
        {
            output = await HandleOrderUpdate(updatedOrder);
            await dbContext.CommitAsync();
            return output;
        }
        catch
        {
            await dbContext.RollbackAsync();
            throw;
        }

    }

    private async Task<OrderModel> HandleOrderUpdate(OrderModel updatedOrder)
    {
        OrderModel output;
        var currentOrder = await GetFullOrder(updatedOrder.Id);

        await HandleProductChanges(currentOrder.OrderedProducts, updatedOrder.OrderedProducts, updatedOrder.Id);
        if (Equals(updatedOrder, currentOrder) == false)
        {
            await orderRepository.UpdateOrder(updatedOrder);
        }
        output = await GetFullOrder(updatedOrder.Id);
        return output;
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

    private async Task HandleProductChanges(IEnumerable<OrderProductModel> currentProducts, IEnumerable<OrderProductModel> updatedProducts, int orderId)
    {
        IEnumerable<OrderProductModel> output;
        var currentDict = currentProducts.ToDictionary(p => p.ProductId);
        var updatedDict = updatedProducts.ToDictionary(p => p.ProductId);

        await HandleProductsDelete(currentDict, updatedDict);
        await HandleProductsInsert(orderId, currentDict, updatedDict);
        await HandleProductsUpdate(orderId, currentDict, updatedDict);
    }

    private async Task HandleProductsDelete(Dictionary<int, OrderProductModel> currentDict, Dictionary<int, OrderProductModel> updatedDict)
    {
        var productsToRemove = currentDict.Keys.Except(updatedDict.Keys);
        foreach (var productId in productsToRemove)
        {
            await orderProductService.DeleteOrderProduct(productId);
        }
    }

    private async Task<IEnumerable<OrderProductModel>> HandleProductsInsert(int orderId, Dictionary<int, OrderProductModel> currentDict, Dictionary<int, OrderProductModel> updatedDict)
    {
        IEnumerable<OrderProductModel> output = Enumerable.Empty<OrderProductModel>();
        var productsToInsert = updatedDict.Keys.Except(currentDict.Keys).Select(key => updatedDict[key]);
        if (productsToInsert.Count() > 0)
        {
            await orderProductService.InsertProductSet(productsToInsert, orderId);
        }
        return output;
    }

    private async Task HandleProductsUpdate(int orderId, Dictionary<int, OrderProductModel> currentDict, Dictionary<int, OrderProductModel> updatedDict)
    {
        var updateCandidates = updatedDict.Keys.Intersect(currentDict.Keys)
            .Select(k => new
            {
                Current = currentDict[k],
                Updated = updatedDict[k]
            });

        var productsToUpdate = updateCandidates
            .Where(p => HasProductQuantitiyChanged(p.Current, p.Updated))
            .Select(p => p.Updated);

        if (productsToUpdate.Count() > 0)
        {
            await orderProductService.UpdateProductSet(productsToUpdate, orderId);
        }
    }

    private bool HasProductQuantitiyChanged(OrderProductModel currect, OrderProductModel updated)
    {
        return currect.Quantity != updated.Quantity;
    }
}
