using Microsoft.AspNetCore.Mvc;
using SharpAITest.API.Mappings;
using SharpAITest.Application.Services.Abstraction;
using SharpAITest.Contracts.DTOs.Orders;
using SharpAITest.Domain.Exceptions;
using SharpAITest.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace SharpAITest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly ILogger<OrdersController> logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        this.orderService = orderService;
        this.logger = logger;
    }

    // GET api/<OrdersController>/5
    [HttpGet()]
    public async Task<ActionResult<OrderResponse>> Get()
    {
        try
        {
            IEnumerable<OrderModel> retrievedOrder = await orderService.GetAllOrders();
            var response = retrievedOrder.ToResponse();
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> Get(int id)
    {
        try
        {
            var retrievedOrder = await orderService.GetFullOrder(id);
            var response = retrievedOrder.ToResponse();
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST api/<OrdersController>
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> Post([FromBody] CreateOrderRequest orderRequest)
    {
        try
        {
            var model = orderRequest.ToModel();
            var order = await orderService.InsertOrder(model);
            var response = order.ToResponse();
            return CreatedAtAction(nameof(Get), new { id = order.Id }, response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT api/<OrdersController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<OrderResponse>> Put([FromBody] UpdateOrderRequest orderRequest)
    {
        try
        {
            var model = orderRequest.ToModel();
            var order = await orderService.UpdateOrder(model);
            var response = order.ToResponse();
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }

    // DELETE api/<OrdersController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await orderService.DeleteOrder(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "Internal server error");
        }
    }
}
