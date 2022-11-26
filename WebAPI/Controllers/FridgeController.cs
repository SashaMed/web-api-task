using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Requests;
using Entities.Responces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.IRepository;
using Services.Contracts;
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
        private IFridgeService _fridgeService;

        public FridgeController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, 
            IMapper mapper, IFridgeService fridgeService)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;   
            _fridgeService = fridgeService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFridges()
        {
            var fridgesDto = await _fridgeService.GetFridges();
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
            var productsDto = await _fridgeService.GetProductsByFridgeId(id, pagingPrameters);
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
            var responce = await _fridgeService.GetFridge(id, request);
            return Ok(responce);
        }


        [Authorize]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFridge([FromBody] FridgeCreationDto fridge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var toReturn = await _fridgeService.CreateFridge(fridge);
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
            await _fridgeService.DeleteFridge(id, fridge);
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
            await _fridgeService.DeleteProductFromFridge(id, productId);
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
            var responce = await _fridgeService.GetProductsNotInFridge(id, pagingPrameters);
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
            await _fridgeService.AddProductsToFridge(id, request);
            return Ok();
        }
    }
}
