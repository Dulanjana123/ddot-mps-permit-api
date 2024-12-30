using DDOT.MPS.Permit.Api.Managers.InspectionManagement;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    public class InspectionController : CoreController
    {
        private readonly IInspectionManager _inspectionManager;

        public InspectionController(IInspectionManager inspectionManager)
        {
            _inspectionManager = inspectionManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateInspection([FromBody] InspectionDto inspectionDto)
        {
            BaseResponse<InspectionResponseDto> createdInspection = await _inspectionManager.CreateInspection(inspectionDto);
            return Ok(createdInspection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInspection(int id, [FromBody] InspectionDto inspectionDto)
        {
            BaseResponse<InspectionResponseDto> updatedInspection = await _inspectionManager.UpdateInspection(id, inspectionDto);
            return Ok(updatedInspection);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInspectionById(int id)
        {
            BaseResponse<InspectionResponseDto> response = await _inspectionManager.GetInspectionById(id);
            return Ok(response);
        }

        [HttpPost("paginated")]
        public async Task<IActionResult> GetPaginatedList([FromBody] InspectionPaginatedRequest request)
        {
            BaseResponse<Result<InspectionResponseDto>> inspectionList = await _inspectionManager.GetPaginatedList(request);
            return Ok(inspectionList);
        }
    }
}
