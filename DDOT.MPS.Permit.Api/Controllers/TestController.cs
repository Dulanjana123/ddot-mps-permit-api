using DDOT.MPS.Permit.Api.Managers.TestManager;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    public class TestController : CoreController
    {
        public readonly ITestManager _testManager;
        private readonly IConfiguration _configuration;
        private readonly string _environmentName;
        private readonly ILogger<TestController> _logger;
        public TestController(ITestManager testManager, IConfiguration configuration, string environmentName, ILogger<TestController> logger)
        {
            _testManager = testManager;
            _configuration = configuration;
            _environmentName = environmentName;
            _logger = logger;
        }

        [HttpGet("check-connection-string")]
        public async Task<IActionResult> CheckConnectionString()
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Controllers.TestController.CheckConnectionString | Request in progress.");
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            _logger.LogInformation("DDOT.MPS.Permit.Api.Controllers.TestController.CheckConnectionString | Environment: {environmentName} | Connection string: {connectionString}", _environmentName, connectionString);
            return Ok($"{_environmentName} : {connectionString}");
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string request)
        {
            return Ok(request);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] string request)
        {
            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            return Ok(id);
        }

        [HttpPost("paginated")]
        public async Task<IActionResult> GetPaginatedList([FromBody] string request)
        {
            return Ok(request);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            return Ok(id);
        }
    }
}
