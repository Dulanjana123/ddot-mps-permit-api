using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class PdrmController : CoreController
    {
        private readonly IPdrmManager _pdrmManager;
        private readonly ILogger<PdrmController> _logger;

        public PdrmController(IPdrmManager pdrmManager, ILogger<PdrmController> logger)
        {
            _pdrmManager = pdrmManager ?? throw new ArgumentNullException(nameof(pdrmManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogInformation("Ctor DDOT.MPS.Permit.Api.Controllers.PdrmController");
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<int>>> CreatePdrm(PdrmCreationRequest pdrmCreationRequest)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Controllers.PdrmController.CreatePdrm | Request in progress. | Pdrm creation request: {pdrmCreationRequest}", JsonSerializer.Serialize(pdrmCreationRequest));
            return Ok(await _pdrmManager.CreatePdrm(pdrmCreationRequest));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PdrmDto>>> GetPdrm(int id)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Controllers.PdrmController.GetPdrm | Request in progress. | Pdrm ID: {id}", id);
            return Ok(await _pdrmManager.GetPdrmById(id));
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<PdrmDto>>> UpdatePdrm(PdrmUpdateRequest pdrmUpdateRequest)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Controllers.PdrmController.UpdatePdrm | Request in progress. | PDRM update rRequest: {updatePdrmDtoRequest}", JsonSerializer.Serialize(pdrmUpdateRequest));
            return Ok();
        }
    }
}
