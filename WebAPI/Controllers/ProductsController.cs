using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using System.ComponentModel.Design;
using Entities.Models;
using WebAPI.Utils.ActionFilters;
using Entities.RequestFeatures;
using Entities.Responces;
using Microsoft.AspNetCore.Authorization;
using Repository.IRepository;
using Services.Contracts;

namespace WebAPI.Controllers
{
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private ILoggerManager _logger;
        private IMapper _mapper;
        private IProductsService _productsService;

        public ProductsController(IRepositoryManager repositoryManager, ILoggerManager loggerManager,
            IMapper mapper, IProductsService productsService)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(RequestParameters pagingPrameters)
        {
            var responce = await _productsService.GetProducts(pagingPrameters);
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
            var toReturn = await _productsService.CreateProduct(fridgeId, product);
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

            await _productsService.DeleteProduct(id);
            return NoContent();
        }

        [Authorize]
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

            await _productsService.UpdateProduct(id, product);
            return NoContent();
        }
    }
}
