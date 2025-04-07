using Microsoft.AspNetCore.Mvc;
using SharpAITest.API.Mappings;
using SharpAITest.Application.Services.Abstraction;
using SharpAITest.Contracts.DTOs.Orders;
using SharpAITest.Contracts.DTOs.Products;
using SharpAITest.Domain.Exceptions;
using SharpAITest.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SharpAITest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ProductResponse>> Get()
        {
            try
            {
                IEnumerable<ProductModel> retrievedProducts = await productService.GetAllProducts();
                var response = retrievedProducts.ToResponse();
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }

        // GET: api/<ProductsController>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> Get(int id)
        {
            try
            {
                var retrievedProduct = await productService.GetProduct(id);
                return Ok(retrievedProduct);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Post([FromBody] CreateProductRequest productRequest)
        {
            try
            {
                var model = productRequest.ToModel();
                var insertedProduct = await productService.InsertProduct(model);
                var response = insertedProduct.ToResponse();
                return CreatedAtAction(nameof(Get), new { id = insertedProduct.Id }, response);
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
        [HttpPut()]
        public async Task<ActionResult<OrderResponse>> Put([FromBody] UpdateProductRequest productRequest)
        {
            try
            {
                var model = productRequest.ToModel();
                var order = await productService.UpdateProduct(model);
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
                await productService.DeleteProduct(id);
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
}
