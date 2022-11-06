using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers
{
    [Route("api/fridges")]
    //[ApiController]
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
        public IActionResult GetFridges()
        {
            var fridges = _repositoryManager.Fridge.GetAllFridges(false);
            var fridgesDto = _mapper.Map<IEnumerable<FridgeDto>>(fridges);
            return Ok(fridgesDto);
        }



        [HttpGet("{id}/products", Name = "CreateProduct")]
        public IActionResult GetProductsByFridgeId(Guid fridgeId)
        {
            var fridge = _repositoryManager.Fridge.GetFridge(fridgeId, trackChanges: false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {fridgeId} doesn't exist in the database.");
                return NotFound();
            }
            var products = _repositoryManager.FridgeProducts.GetFridgeProducts(fridgeId);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }



        [HttpGet("{id}", Name = "FridgeById")]
        public IActionResult GetFridge(Guid id)
        {
            var fridge = _repositoryManager.Fridge.GetFridge(id, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var fridgeModel = _repositoryManager.FridgeModel.GetFridgeModel(fridge.FridgeModelId, false);
            fridge.FridgeModel = fridgeModel;
            var fridgeDto = _mapper.Map<FridgeDto>(fridge);
            return Ok(fridgeDto);
        }


        [HttpPost]
        public IActionResult CreateFridge([FromBody] FridgeCreationDto fridge)
        {
            if (fridge == null)
            {
                _logger.LogError("FridgeCreationDto object sent from client is null.");
                return BadRequest("FridgeCreationDto object is null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var fridgeEntity = _mapper.Map<Fridge>(fridge);

            _repositoryManager.Fridge.CreateFridge(fridgeEntity);
            _repositoryManager.Save();

            var toReturn = _mapper.Map<FridgeDto>(fridgeEntity);
            return CreatedAtRoute("FridgeById", new {id = toReturn.Id}, toReturn);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteFridge(Guid id)
        {
            var fridge = _repositoryManager.Fridge.GetFridge(id, false);
            if (fridge == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repositoryManager.FridgeProducts.DeleteFridgeProducts(id);
            _repositoryManager.Fridge.DeleteFridge(fridge);
            _repositoryManager.Save();

            return NoContent();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateFridge(Guid id, [FromBody] FridgeCreationDto fridge)
        {
            if (fridge == null)
            {
                _logger.LogError("FridgeCreationDto object sent from client is null.");
                return BadRequest("FridgeCreationDto object is null.");
            }
            
            var fridgeEntity = _repositoryManager.Fridge.GetFridge(id, true);
            if (fridgeEntity == null)
            {
                _logger.LogInfo($"Fridge with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the FridgeCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(fridge, fridgeEntity);
            _repositoryManager.Save();
            return NoContent();
        }
        
    }
}
