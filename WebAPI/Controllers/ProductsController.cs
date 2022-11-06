using AutoMapper;
using Contracts.IRepository;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using System.ComponentModel.Design;
using Entities.Models;
using WebAPI.Utils.ActionFilters;
using Entities.RequestFeatures;

namespace WebAPI.Controllers
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public ProductsController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(RequestParameters pagingPrameters)
        {
            var products = await _repositoryManager.Products.GetAllProductsAsync(pagingPrameters, false);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductByFridgeId( Guid productId)
        {
            var product = await _repositoryManager.Products.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
                return NotFound();
            }
            var productsDto = _mapper.Map<ProductDto>(product);
            return Ok(productsDto);
        }


        [HttpPost("{fridgeId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProduct(Guid fridgeId, [FromBody] ProductCreationDto product)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(fridgeId, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            var productEntity = _mapper.Map<Product>(product);
            _repositoryManager.Products.CreateProduct(productEntity);
            _repositoryManager.FridgeProducts.CreateFridgeProduct(productEntity.Id, fridgeId);
            await _repositoryManager.SaveAsync();

            var toReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("GetProductByFridgeId", new { fridgeId, id = toReturn.Id }, toReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repositoryManager.Products.GetProductAsync(id, false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repositoryManager.Products.DeleteProduct(product);
            _repositoryManager.FridgeProducts.DeleteProduct(id);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductCreationDto product)
        {
            var productEntity = await _repositoryManager.Products.GetProductAsync(id, true);
            if (productEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(product, productEntity);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }

        [HttpGet("procedure")]
        public async Task<IActionResult> ExecStoredProcedure()
        {
            var quantity = await _repositoryManager.CallStoredProcedure();
            return Ok($"{quantity} rows affected during execution");
        }
    }
}
