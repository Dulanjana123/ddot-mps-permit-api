using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers.SwoManagement
{
    public interface ISwoManager
    {
        Task<BaseResponse<SwoResponseDto>> CreateSwoViolation(SwoViolationDto swoViolation);

        Task<BaseResponse<SwoResponseDto>> UpdateSwoViolation(int id, SwoViolationDto swoViolation);

        Task<BaseResponse<SwoViolationTypesResponseDto>> GetViolationTypes();

    }
}
