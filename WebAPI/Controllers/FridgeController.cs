using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
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
            var fridges = await _repositoryManager.Fridge.GetAllFridgesAsync(false);
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
        public async Task<IActionResult> GetFridge(Guid id)
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
            return Ok(fridgeDto);
        }


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


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateFridge(Guid id, [FromBody] FridgeCreationDto fridge)
        {
            var fridgeEntity = _repositoryManager.Fridge.GetFridgeAsync(id, true);
            if (fridgeEntity == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            await _mapper.Map(fridge, fridgeEntity);
            await _repositoryManager.SaveAsync();
            return NoContent();
        }
        
    }
}
