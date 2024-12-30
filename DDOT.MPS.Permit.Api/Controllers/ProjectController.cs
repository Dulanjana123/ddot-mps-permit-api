using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class ProjectController : CoreController
    {
        private readonly IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager) => _projectManager = projectManager;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectAddDto request) => Ok(await _projectManager.Create(request));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _projectManager.GetById(id));

        [HttpPost("paginated")]
        public async Task<IActionResult> GetPaginated([FromBody] ProjectPaginatedRequest request) => Ok(await _projectManager.GetPaginatedList(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectUpdateDto request) => Ok(await _projectManager.Update(id, request));
    }
}
