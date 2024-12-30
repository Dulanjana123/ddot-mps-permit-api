using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers
{
    public interface IEwrManager
    {
        Task<BaseResponse<Result<EwrResponseDto>>> GetPaginatedList(EwrPaginatedRequest request);

        Task<BaseResponse<EwrDashboardResponseDto>> GetEwrDashboardData(DateRangeRequest request);

        Task<BaseResponse<EwrResponseDto>> GetById(int id);

        Task<BaseResponse<AssigningInfoResponseDto>> GetAssigningInfo();

        Task<BaseResponse<EwrAssigningDto>> UpdateEwrAssigning(int id, EwrAssigningDto ewrAssigningDto);

        Task<BaseResponse<EwrBulkAssigningDto>> UpdateEwrBulkAssigning(EwrBulkAssigningDto ewrBulkAssigningDto);

        Task<BaseResponse<EwrIndexFiltersInfoResponseDto>> GetIndexFiltersInfo();
        Task<BaseResponse<EwrCreateResponse>> CreateEwr(EwrCreateRequest ewrAssigningDto);

        Task<BaseResponse<EwrCloseDto>> CloseEwr(int id, EwrCloseDto ewrCloseDto);
        Task<BaseResponse<Result<EwrResponseDto>>> GetEWRByLocation(EwrLocationFilterRequest locationFilterRequest);

    }
}
