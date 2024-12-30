using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers.InspectionManagement
{
    public interface IInspectionManager
    {
        Task<BaseResponse<InspectionResponseDto>> CreateInspection(InspectionDto inspection);

        Task<BaseResponse<InspectionResponseDto>> UpdateInspection(int id, InspectionDto inspection);

        Task<BaseResponse<InspectionResponseDto>> GetInspectionById(int id);

        Task<BaseResponse<Result<InspectionResponseDto>>> GetPaginatedList(InspectionPaginatedRequest request);
    }
}
