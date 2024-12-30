using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class EwrController : CoreController
    {
        private readonly IEwrManager _ewrManager;

        public EwrController(IEwrManager ewrManager)
        {
            _ewrManager = ewrManager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            BaseResponse<EwrResponseDto> project = await _ewrManager.GetById(id);
            return Ok(project);
        }

        [HttpPost("paginated")]
        public async Task<IActionResult> GetPaginatedList([FromBody] EwrPaginatedRequest request)
        {
            BaseResponse<Result<EwrResponseDto>> ewrListResponse = await _ewrManager.GetPaginatedList(request);
            return Ok(ewrListResponse);
        }

        [HttpPost("dashboard")]
        public async Task<IActionResult> GetQuickDashboardData([FromBody] DateRangeRequest request)
        {

            BaseResponse<EwrDashboardResponseDto> ewrDashboardData = await _ewrManager.GetEwrDashboardData(request);
            return Ok(ewrDashboardData);
        }

        [HttpGet("assigning-info")]
        public async Task<IActionResult> GetAssigningInfo()
        {
            BaseResponse<AssigningInfoResponseDto> assigningInfo = await _ewrManager.GetAssigningInfo();
            return Ok(assigningInfo);
        }

        [HttpPut("assign/{id}")]
        public async Task<IActionResult> UpdateEwrAssigning(int id, [FromBody] EwrAssigningDto ewrAssigningDto)
        {
            BaseResponse<EwrAssigningDto> updatedEwrAssigning = await _ewrManager.UpdateEwrAssigning(id, ewrAssigningDto);
            return Ok(updatedEwrAssigning);
        }

        [HttpPut("assign/bulk")]
        public async Task<IActionResult> UpdateEwrBulkAssigning([FromBody] EwrBulkAssigningDto ewrBulkAssigningDto)
        {
            BaseResponse<EwrBulkAssigningDto> updatedEwrBulkAssigning = await _ewrManager.UpdateEwrBulkAssigning(ewrBulkAssigningDto);
            return Ok(updatedEwrBulkAssigning);
        }

        [HttpGet("index-filters-info")]
        public async Task<IActionResult> GetIndexFiltersInfo()
        {
            BaseResponse<EwrIndexFiltersInfoResponseDto> indexFiltersInfo = await _ewrManager.GetIndexFiltersInfo();
            return Ok(indexFiltersInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EwrCreateRequest ewrNewRequest)
        {
            BaseResponse<EwrCreateResponse> createdEwr = await _ewrManager.CreateEwr(ewrNewRequest);
            return Ok(createdEwr);
        }

        [HttpPut("close/{id}")]
        public async Task<IActionResult> CloseEwr(int id, [FromBody] EwrCloseDto ewrCloseDto)
        {
            BaseResponse<EwrCloseDto> closedEwr = await _ewrManager.CloseEwr(id, ewrCloseDto);
            return Ok(closedEwr);
        }

        [HttpPost("location")]
        public async Task<IActionResult> GetEWRByLocation(EwrLocationFilterRequest locationFilterRequest)
        {
            BaseResponse<Result<EwrResponseDto>> ewrList = await _ewrManager.GetEWRByLocation(locationFilterRequest);
            return Ok(ewrList);
        }

    }
}
