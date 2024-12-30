

using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository
{
    public interface IMpsEwrRepository
    {
        IQueryable<EwrResponseDto> GetAll(EwrPaginatedRequest request);
        Task<EwrApplication> GetById(int id);

        Task<List<EwrApplication>> GetByIdList(List<int> ids);

        long GetRowCount(EwrPaginatedRequest request);

        long GetTotalEwrCount(DateRangeRequest request);

        long EwrCountWithoutCP(DateRangeRequest request);

        IQueryable<AgencyEwrCountDto> GetEwrCountByUtilityCompany(DateRangeRequest request);

        IQueryable<WardEwrCountDto> GetEwrCountByWard(DateRangeRequest request);

        IQueryable<EmergencyTypeEwrCountDto> GetEwrCountByEmergencyType(DateRangeRequest request);
        IQueryable<InspectorCountDto> GetInspectedCountByInspector(DateRangeRequest request);

        IQueryable<StatusEwrCountDto> GetEwrCountByStatus(DateRangeRequest request);

        IQueryable<EwrStatusOption> GetAllEwrStatuses();

        IQueryable<EwrEmergencyTypeOption> GetAllEwrEmergencyTypes();

        IQueryable<EwrEmergencyCauseOption> GetAllEwrEmergencyCauses();
        IQueryable<EwrEmergencyCategoryOption> GetAllEwrEmergencyCategories();

        Task<EwrApplication> UpdateEwrApplication(EwrApplication ewrApplication);

        Task<IList<EwrApplication>> UpdateEwrApplications(List<EwrApplication> ewrApplications);

        Task<EwrApplication> CreateEwr(EwrCreateRequest ewrApplication);

        IQueryable<EwrResponseDto> GetAllByDateRange(DateRangeRequest request);

        List<EwrResponseDto> GetEWRByLocation(EwrPaginatedRequest request);

    }
}
