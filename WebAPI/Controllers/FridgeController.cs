using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;

namespace WebAPI.Controllers
{
    [Route("api/fridges")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private ILoggerManager _logger;

        public FridgeController(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
        }

        [HttpGet]
        public IActionResult GetFridges()
        {
            try
            {
                var fridges = _repositoryManager.Fridge.GetAllFridges(false);
                return Ok(fridges);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetFridges)}action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
    }
}
