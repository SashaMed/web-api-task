using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Requests;
using Entities.Responces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using WebAPI.Utils.ActionFilters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers
{
    [Route("api/fridges")]
    public class FridgeController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public FridgeController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetFridges()
        {
            var fridges = await _repositoryManager.Fridge.GetAllFridgesWithModels(false);
            var fridgesDto = _mapper.Map<IEnumerable<FridgeDto>>(fridges);
            return Ok(fridgesDto);
        }



        [HttpGet("{id}/products", Name = "GetProductByFridgeId")]
        public async Task<IActionResult> GetProductsByFridgeId(Guid id, RequestParameters pagingPrameters)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var products = await _repositoryManager.FridgeProducts.GetFridgeProductsAsync(id, pagingPrameters);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }



        [HttpGet("{id}", Name = "FridgeById")]
        public async Task<IActionResult> GetFridge(Guid id, RequestParameters request)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeModel = await _repositoryManager.FridgeModel.GetFridgeModelAsync(fridge.FridgeModelId, false);
            fridge.FridgeModel = fridgeModel;
            var fridgeDto = _mapper.Map<FridgeDto>(fridge);
            var responce = new GetFridgeDetailsResponce
            {
                Fridge = fridgeDto,
                Products = await _repositoryManager.FridgeProducts.GetFridgeProductsAsync(id, request),
                ProductsCount = await _repositoryManager.FridgeProducts.GetFridgeProductsCountAsync(id)
            };
            return Ok(responce);
        }


        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridge([FromBody] FridgeCreationDto fridge)
        {
            var fridgeEntity = _mapper.Map<Fridge>(fridge);
            _repositoryManager.Fridge.CreateFridge(fridgeEntity);
            await _repositoryManager.SaveAsync();
            var toReturn = _mapper.Map<FridgeDto>(fridgeEntity);
            return CreatedAtRoute("FridgeById", new {id = toReturn.Id}, toReturn);
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridge(Guid id)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repositoryManager.FridgeProducts.DeleteFridgeProducts(id);
            _repositoryManager.Fridge.DeleteFridge(fridge);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }


        [Authorize]
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateFridge(Guid id, [FromBody] FridgeCreationDto fridge)
        {
            var fridgeEntity = await _repositoryManager.Fridge.GetFridgeAsync(id, true);
            if (fridgeEntity == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(fridge, fridgeEntity);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }

        [HttpGet("models")]
        public async Task<IActionResult> GetFridgeModels()
        {
            var models = await _repositoryManager.FridgeModel.GetAllFridgeModelsAsync(false);
            return Ok(models);
        }

        [Authorize]
        [HttpDelete("{id}/products/{productId}")]
        public async Task<IActionResult> DeleteProductFromFridge(Guid id,Guid productId)
        {
            var product = await _repositoryManager.Products.GetProductAsync(productId, false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
                return NotFound();
            }
            _repositoryManager.FridgeProducts.DeleteProductFromFridge(productId, id);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }


        [HttpGet("{id}/products/outside")]
        public async Task<IActionResult> GetProductsNotInFridge(Guid id, RequestParameters pagingPrameters)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeDto = _mapper.Map<FridgeDto>(fridge);
            var products = await _repositoryManager.FridgeProducts.GetProductsNotFromFridge(id, pagingPrameters);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            var responce = new GetProductsNotInFridgeResponce
            {
                Products = productsDto,
                Fridge = fridgeDto,
                ProductsCount = await _repositoryManager.FridgeProducts.GetProductsNotFromFridgeCount(id)
            };
            return Ok(responce);
        }

        [Authorize]
        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProductsToFridge(Guid id, [FromBody] AddProductsRequest request)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            if (request.QuantityList.Count() != request.Guids.Count())
            {
                _logger.LogInfo($"Length of lists in request does not match");
                return BadRequest();
            }
            var guids = (List<Guid>)request.Guids;
            var quantityes = (List<int>)request.QuantityList;
            for (int i = 0; i < guids.Count(); i++)
            {
                _repositoryManager.FridgeProducts.CreateFridgeProduct(productId:guids[i], fridgeId:id, quantityes[i]);
            }
            await _repositoryManager.SaveAsync();
            return Ok();
        }
    }
}
