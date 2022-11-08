using AutoMapper;
using Contracts.IRepository;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/procedures")]
    [ApiController]
    public class ProceduresController : ControllerBase
    {
        private IRepositoryManager _repositoryManager;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public ProceduresController(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }


        [HttpGet("procedure")]
        public async Task<IActionResult> ExecStoredProcedure()
        {
            var quantity = await _repositoryManager.CallStoredProcedure();
            return Ok($"{quantity} rows affected during execution");
        }
    }
}
