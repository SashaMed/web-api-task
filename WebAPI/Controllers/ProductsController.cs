using AutoMapper;
using Contracts.IRepository;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using System.ComponentModel.Design;
using Entities.Models;

namespace WebAPI.Controllers
{
    [Route("api/products")]
    //[ApiController]
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
        public IActionResult GetProducts()
        {
            var products = _repositoryManager.Products.GetAllProducts(false);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }


        [HttpGet("{id}")]
        public IActionResult GetSingleProductByFridgeId( Guid productId)
        {
            var product = _repositoryManager.Products.GetProduct(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
                return NotFound();
            }
            var productsDto = _mapper.Map<ProductDto>(product);
            return Ok(productsDto);
        }


        [HttpPost("{fridgeId}")]
        public IActionResult CreateProduct(Guid fridgeId, [FromBody] ProductCreationDto product)
        {
            if (product == null)
            {
                _logger.LogError("ProductCreationDto object sent from client is null.");
                return BadRequest("ProductCreationDto object is null.");
            }

            var fridge = _repositoryManager.Fridge.GetFridge(fridgeId, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the ProductCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var productEntity = _mapper.Map<Product>(product);
            _repositoryManager.Products.CreateProduct(productEntity);
            _repositoryManager.FridgeProducts.CreateFridgeProduct(productEntity.Id, fridgeId);
            _repositoryManager.Save();

            var toReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("CreateProduct", new { fridgeId, id = toReturn.Id }, toReturn);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _repositoryManager.Products.GetProduct(id, false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repositoryManager.Products.DeleteProduct(product);
            _repositoryManager.Save();
            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, [FromBody] ProductCreationDto product)
        {
            if (product == null)
            {
                _logger.LogError("ProductCreationDto object sent from client is null.");
                return BadRequest("ProductCreationDto object is null.");
            }

            var productEntity = _repositoryManager.Products.GetProduct(id, true);
            if (productEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the ProductCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(product, productEntity);
            _repositoryManager.Save();
            return NoContent();
        }
    }
}
