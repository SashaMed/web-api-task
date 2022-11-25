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
using Entities.Responces;
using Microsoft.AspNetCore.Authorization;

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
            //var currentUrl = HttpContext.Current.Request;

			var products = await _repositoryManager.Products.GetAllProductsAsync(pagingPrameters, false);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            var productCount = await _repositoryManager.Products.GetProductCountAsync();
            var responce = new GetAllProductsResponce
            {
                Products = productsDto,
                ProductsCount = productCount
            };
            return Ok(responce);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleProductById( Guid id)
        {
            var product = await _repositoryManager.Products.GetProductAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var productsDto = _mapper.Map<ProductDto>(product);
            return Ok(productsDto);
        }

        [Authorize]
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
            _repositoryManager.FridgeProducts.CreateFridgeProduct(productEntity.Id, fridgeId, product.Quantity);
            await _repositoryManager.SaveAsync();

            var toReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("GetProductByFridgeId", new { fridgeId, id = toReturn.Id }, toReturn);
        }

        [Authorize]
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

        //[Authorize]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductUpdateDto product)
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





    }
}
