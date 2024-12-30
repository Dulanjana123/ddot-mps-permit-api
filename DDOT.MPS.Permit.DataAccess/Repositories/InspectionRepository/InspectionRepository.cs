using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;

        public InspectionRepository(MpsDbContext userDbContext, IMapper mapper)
        {
            _dbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<InspectionDto> CreateInspection(InspDetail inspectionDetail)
        {
            _dbContext.Add(inspectionDetail);
            await _dbContext.SaveChangesAsync();
            InspectionDto newInspection = _mapper.Map<InspectionDto>(inspectionDetail);
            return newInspection;
        }

        public async Task<IList<InspDetail>> CreateBulk(List<InspDetail> inspectionDetailList)
        {
            // Bulk insert logic will be added...
            // This is under development...
            return inspectionDetailList;
        }

        public IQueryable<InspectionResponseDto> GetAll(InspectionPaginatedRequest request)
        {
            IQueryable<InspDetail> inspections = _dbContext.InspDetails.Include(req => req.InspectedByNavigation);

            if (request.Filters.EwrRequestId != null) inspections = inspections.Where(req => req.ApplicationId.Equals(request.Filters.EwrRequestId));

            IQueryable<InspectionResponseDto> finalizedInspectionResponse = inspections

            .OrderByDescending(req => req.ModifiedDate.HasValue ? req.ModifiedDate : req.CreatedDate)
            .Skip((request.PagingAndSortingInfo.Paging.PageNo - 1) * request.PagingAndSortingInfo.Paging.PageSize)
            .Take(request.PagingAndSortingInfo.Paging.PageSize)
            .Select(req => new InspectionResponseDto
            {
                ApplicationId = req.ApplicationId,
                InspTypeId = req.InspTypeId,
                InspectedBy = req.InspectedBy,
                InspDetailId = req.InspDetailId,
                InspectionDate = req.InspectionDate,
                Inspector = string.Concat(req.InspectedByNavigation.FirstName, " ", req.InspectedByNavigation.LastName),
                ApplicationTypeId = req.ApplicationTypeId,
                CreatedDate = req.CreatedDate,
                ExternalNotes = req.ExternalNotes,
                InspStatusId = req.InspStatusId,
                InternalNotes = req.InternalNotes,
                MinutesSpent = req.MinutesSpent,
            }
            )
            .AsQueryable();

            return finalizedInspectionResponse;
        }

        public IQueryable<InspStatusOption> GetAllInspectionStatuses()
        {
            IQueryable<InspStatusOption> inspStatuses = _dbContext.InspStatuses
            .Select(res => new InspStatusOption
            {
                InspStatusId = res.InspStatusId,
                InspStatusDesc = res.InspStatusDesc,
            }).AsQueryable();

            return inspStatuses;
        }

        public async Task<InspDetail> GetById(int id)
        {
            return await _dbContext.InspDetails.FirstOrDefaultAsync(req => req.InspDetailId == id);
        }

        public async Task<InspectionResponseDto> GetInspectionById(int id)
        {
            InspectionResponseDto? inspection = await _dbContext.InspDetails
               .Include(req => req.InspectedByNavigation)
               .Select(req => new InspectionResponseDto
               {
                   ApplicationId = req.ApplicationId,
                   InspTypeId = req.InspTypeId,
                   InspectedBy = req.InspectedBy,
                   InspDetailId = req.InspDetailId,
                   InspectionDate = req.InspectionDate,
                   Inspector = string.Concat(req.InspectedByNavigation.FirstName, " ", req.InspectedByNavigation.LastName),
                   ApplicationTypeId = req.ApplicationTypeId,
                   CreatedDate = req.CreatedDate,
                   ExternalNotes = req.ExternalNotes,
                   InspStatusId = req.InspStatusId,
                   InternalNotes = req.InternalNotes,
                   MinutesSpent = req.MinutesSpent,
               }
               )
               .SingleOrDefaultAsync(req => req.InspDetailId == id);
            return inspection;
        }

        public long GetRowCount(InspectionPaginatedRequest request)
        {
            IQueryable<InspDetail> inspections = _dbContext.InspDetails;
            if (request.Filters.EwrRequestId != null) inspections = inspections.Where(req => req.ApplicationId.Equals(request.Filters.EwrRequestId));
            return inspections.Count();
        }

        public async Task<InspDetail> UpdateInspection(InspDetail inspectionDetail)
        {
            _dbContext.Update(inspectionDetail);
            await _dbContext.SaveChangesAsync();
            return inspectionDetail;
        }
    }
}
