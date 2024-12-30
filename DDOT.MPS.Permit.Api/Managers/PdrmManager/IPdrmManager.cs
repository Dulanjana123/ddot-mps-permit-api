
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers
{
    public interface IPdrmManager
    {
        Task<BaseResponse<int>> CreatePdrm(PdrmCreationRequest meetingCreationRequest);
        Task<BaseResponse<PdrmDto>> GetPdrmById(int id);
    }
}
