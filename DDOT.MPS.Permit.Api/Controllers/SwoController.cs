using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Api.Managers.SwoManagement;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class SwoController : CoreController
    {
        private readonly ISwoManager _swoManager;

        public SwoController(ISwoManager swoManager)
        {
            _swoManager = swoManager;
        }

        [HttpGet("violation-types")]
        public async Task<IActionResult> GetViolationTypes()
        {
            BaseResponse<SwoViolationTypesResponseDto> violationTypes = await _swoManager.GetViolationTypes();
            return Ok(violationTypes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSwo([FromBody] SwoViolationDto swoViolationDto)
        {
            BaseResponse<SwoResponseDto> createdSwoViolation = await _swoManager.CreateSwoViolation(swoViolationDto);
            return Ok(createdSwoViolation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSwo(int id, [FromBody] SwoViolationDto swoViolationDto)
        {
            BaseResponse<SwoResponseDto> updatedSwoViolation = await _swoManager.UpdateSwoViolation(id, swoViolationDto);
            return Ok(updatedSwoViolation);
        }

    }
}
