using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository
{
    public interface IInspectionRepository
    {
        Task<InspectionDto> CreateInspection(InspDetail inspectionDetail);

        Task<IList<InspDetail>> CreateBulk(List<InspDetail> inspectionDetailList);

        Task<InspectionResponseDto> GetInspectionById(int id);

        Task<InspDetail> GetById(int id);

        Task<InspDetail> UpdateInspection(InspDetail inspectionDetail);

        IQueryable<InspectionResponseDto> GetAll(InspectionPaginatedRequest request);

        long GetRowCount(InspectionPaginatedRequest request);

        IQueryable<InspStatusOption> GetAllInspectionStatuses();
    }
}
