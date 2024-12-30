using DDOT.MPS.Permit.DataAccess.Repositories;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers
{
    public class PdrmManager: IPdrmManager
    {
        private readonly IPdrmRepository _pdrmRepository;
        private readonly ILogger<PdrmManager> _logger;

        public PdrmManager(IPdrmRepository pdrmRepository, ILogger<PdrmManager> logger)
        {
            _pdrmRepository = pdrmRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<int>> CreatePdrm(PdrmCreationRequest pdrmCreationRequest)
        {
            int pdrMeetingType = pdrmCreationRequest.Type;
            //Do a validation meetingCreationRequest.Type == pdrm meeting type

            int pdrmId = await _pdrmRepository.CreatePdrm(pdrMeetingType);

            return new BaseResponse<int>
            {
                Success = true,
                Data = pdrmId,
                Message = "PDRM_CREATED_SUCCESSFULLY"
            };
        }

        public async Task<BaseResponse<PdrmDto>> GetPdrmById(int id)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.PdrmManager.GetPdrmById | Request in progress. | Pdrm ID: {id} id", id);
            if (id < 1)
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.PdrmManager.GetPdrmById | Requested pdrm ID is out of range. | Pdrm ID: {id} id", id);
                throw new UDArgumentException("PDRM_INVALID_ID");
            }

            if (!await _pdrmRepository.PdrmExists(id))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.PdrmManager.GetPdrmById | Requested pdrm ID does not exist. | Pdrm ID: {id} id", id);
                throw new UDArgumentException("PDRM_DOES_NOT_EXIST");
            }

            PdrmDto pdrmDto = await _pdrmRepository.GetPdrmById(id);
            //pdrmDto.Quadrants =
            //pdrmDto.LocationTypes =

            return new BaseResponse<PdrmDto> 
            { 
                Success = true,
                Message = "PDRM_SUCCESSFULLY_RETRIEVED",
                Data = pdrmDto
            };
        }
    }
}
