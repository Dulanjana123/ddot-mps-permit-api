using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository
{
    public class MpsEwrRepository : IMpsEwrRepository
    {
        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;

        public MpsEwrRepository(MpsDbContext userDbContext, IMapper mapper)
        {
            _dbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<EwrApplication> GetById(int id)
        {
            EwrApplication? mpsEwrApplication = await _dbContext.EwrApplications
                .Include(req => req.EmergencyType)
                .Include(req => req.EmergencyCause)
                .Include(req => req.EmergencyType)
                .Include(req => req.AppliedByNavigation)
                .Include(req => req.AssignedInspectorNavigation)
                .Include(req => req.EwrApplicationLocations)
                    .ThenInclude(reqLoc => reqLoc.EwrLocation)
                        .ThenInclude(location => location.LocationCategory)
                .Include(req => req.EwrApplicationLocations)
                    .ThenInclude(reqLoc => reqLoc.EwrLocation)
                        .ThenInclude(location => location.LocationType)
                .Include(req => req.Status)
                .Include(req => req.Tcp)
                .Include(req => req.Agency)
                .SingleOrDefaultAsync(req => req.EwrApplicationId == id);
            mpsEwrApplication = _mapper.Map<EwrApplication>(mpsEwrApplication);
            return mpsEwrApplication;
        }

        public async Task<List<EwrApplication>> GetByIdList(List<int> ids)
        {
            List<EwrApplication> ewrApplications = await _dbContext.EwrApplications
                                          .Where(e => ids.Contains(e.EwrApplicationId))
                                          .ToListAsync();
            return ewrApplications;
        }

        public IQueryable<EwrResponseDto> GetAll(EwrPaginatedRequest request)
        {
            IQueryable<EwrApplication> ewrApplications = _dbContext.EwrApplications
                .Include(req => req.EmergencyType)
                .Include(req => req.EmergencyCause)
                .Include(req => req.EmergencyCategory)
                .Include(req => req.AppliedByNavigation)
                .Include(req => req.AssignedInspectorNavigation)
                .Include(req => req.EwrApplicationLocations)
                    .ThenInclude(reqLoc => reqLoc.EwrLocation)
                .Include(req => req.Status)
                .Include(req => req.Tcp)
                .Include(req => req.Agency)
                .Include(req => req.SwoApplication)

                .Where(req =>
                    req.EwrRequestNumber.ToLower().Contains(request.Filters.RequestNumber.Trim().ToLower()) &&
                    req.EwrApplicationLocations.Any(recLoc => recLoc.EwrLocation.FullDescription.ToLower().Contains(request.Filters.Location.Trim().ToLower())) &&
                    req.EwrApplicationLocations.Any(recLoc => recLoc.EwrLocation.Ward.ToLower().Contains(request.Filters.Ward.Trim().ToLower())) &&
                    (req.EmergencyType.EmergencyTypeDesc == null || req.EmergencyType.EmergencyTypeDesc.ToLower().Contains(request.Filters.EmergencyType.Trim().ToLower())) &&
                    (req.EmergencyCause.EmergencyCauseDesc == null || req.EmergencyCause.EmergencyCauseDesc.ToLower().Contains(request.Filters.EmergencyCause.Trim().ToLower())) &&
                    (req.Status.StatusDesc == null || req.Status.StatusDesc.ToLower().Contains(request.Filters.Status.Trim().ToLower())) &&
                    (req.AppliedByNavigation.EmailAddress == null || req.AppliedByNavigation.EmailAddress.ToLower().Contains(request.Filters.AppliedBy.Trim().ToLower())) &&
                    (req.Agency.AgencyName == null || req.Agency.AgencyName.ToLower().Contains(request.Filters.UtilityCompany.Trim().ToLower())) &&
                    (req.ClientReferenceNum == null || req.ClientReferenceNum.ToLower().Contains(request.Filters.InternalUtilityTrackingNumber.Trim().ToLower())) &&
                    (req.AssignedInspectorNavigation.FirstName == null || req.AssignedInspectorNavigation.LastName == null || string.Concat(req.AssignedInspectorNavigation.FirstName, " ", req.AssignedInspectorNavigation.LastName).ToLower().Contains(request.Filters.AssignedInspector.Trim().ToLower())) &&
                    (!request.Filters.AssignedInspectorId.HasValue || req.AssignedInspector.Equals(request.Filters.AssignedInspectorId)) &&
                    (!request.Filters.EwrStatusId.HasValue || req.StatusId.Equals(request.Filters.EwrStatusId)) &&
                    (!request.Filters.RequestedDate.HasValue || req.SubmissionDate.Equals(request.Filters.RequestedDate)) &&
                    //To identify conflicting EWRs
                    (req.EwrApplicationId == null || req.EwrApplicationId != request.Filters.ExceptEwrRequestId) &&
                    (!request.Filters.LocationId.HasValue || req.EwrApplicationLocations.Any(reqLoc => reqLoc.EwrLocationId.Equals(request.Filters.LocationId))) &&
                    // Filter on id lists
                    (request.Filters.SwoStatusIds == null || request.Filters.SwoStatusIds.Contains(req.SwoApplication.SwoStatusId ?? 0)) &&
                    (request.Filters.IssuedByIds == null || request.Filters.IssuedByIds.Contains(req.IssuedBy ?? 0))
                );

            if (request.Filters.EffectiveDate != null) ewrApplications = ewrApplications.Where(req => req.EffectiveDate >= request.Filters.EffectiveDate);
            if (request.Filters.ExpirationDate != null) ewrApplications = ewrApplications.Where(req => req.ExpirationDate <= request.Filters.ExpirationDate);

            if (request.Filters.StartDate != null) ewrApplications = ewrApplications.Where(req => req.SubmissionDate >= request.Filters.StartDate);
            if (request.Filters.EndDate != null) ewrApplications = ewrApplications.Where(req => req.SubmissionDate <= request.Filters.EndDate);

            if (request.Filters.CreationDate != null) ewrApplications = ewrApplications.Where(req => req.CreatedDate >= request.Filters.CreationDate);
            if (request.Filters.LastInspectionDate != null) ewrApplications = ewrApplications.Where(req => req.LastInspectionDate >= request.Filters.LastInspectionDate);

            IQueryable<EwrResponseDto> finalizedEwrRequests = ewrApplications.OrderByDescending(req => req.SubmissionDate.HasValue ? req.SubmissionDate : req.CreatedDate)
            .Skip((request.PagingAndSortingInfo.Paging.PageNo - 1) * request.PagingAndSortingInfo.Paging.PageSize)
            .Take(request.PagingAndSortingInfo.Paging.PageSize)
            .Select(req => new EwrResponseDto
            {
                RequestId = req.EwrApplicationId,
                RequestNumber = req.EwrRequestNumber,
                Location = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.FullDescription).FirstOrDefault(),
                Ward = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Ward).FirstOrDefault(),
                EffectiveDate = req.EffectiveDate,
                ExpirationDate = req.ExpirationDate,
                EmergencyType = req.EmergencyType.EmergencyTypeDesc,
                EmergencyCause = req.EmergencyCause.EmergencyCauseDesc,
                Status = req.Status.StatusDesc,
                AppliedBy = string.Concat(req.AppliedByNavigation.FirstName, " ", req.AppliedByNavigation.LastName),
                CreationDate = req.CreatedDate,
                UtilityCompany = req.Agency.AgencyName,
                InternalUtilityTrackingNumber = req.ClientReferenceNum,
                AssignedInspector = string.Concat(req.AssignedInspectorNavigation.FirstName, " ", req.AssignedInspectorNavigation.LastName),
                LastInspectionDate = req.LastInspectionDate,
                XCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.XCoord).FirstOrDefault(),
                YCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.YCoord).FirstOrDefault(),
                Latitude = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Latitude).FirstOrDefault(),
                Longitude = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Longitude).FirstOrDefault(),
                MarXCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.MarXCoord).FirstOrDefault(),
                MarYCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.MarYCoord).FirstOrDefault(),
            }
            )
            .AsQueryable();

            return finalizedEwrRequests;
        }

        public long GetRowCount(EwrPaginatedRequest request)
        {
            return _dbContext.EwrApplications.Where(x =>
               x.EwrRequestNumber.Contains(request.Filters.RequestNumber.Trim()) &&
               (x.EwrApplicationId == null || x.EwrApplicationId != request.Filters.ExceptEwrRequestId) &&
                    (!request.Filters.LocationId.HasValue || x.EwrApplicationLocations.Any(reqLoc => reqLoc.EwrLocationId.Equals(request.Filters.LocationId)))
           ).Count();
        }

        public long GetTotalEwrCount(DateRangeRequest request)
        {
            return _dbContext.EwrApplications.Where(x =>
               x.CreatedDate >= request.StartDate && x.CreatedDate <= request.EndDate).Count();
        }

        public long EwrCountWithoutCP(DateRangeRequest request)
        {
            return _dbContext.EwrApplications.Where(ewr =>
               ewr.CreatedDate >= request.StartDate && ewr.CreatedDate <= request.EndDate)
                .Where(ewr => !_dbContext.CpApplications.
                Any(cp => cp.EwrApplicationId == ewr.EwrApplicationId && cp.IsEwrAssociated == true)).Count();
        }

        public IQueryable<AgencyEwrCountDto> GetEwrCountByUtilityCompany(DateRangeRequest request)
        {
            IQueryable<AgencyEwrCountDto> ewrApplications = _dbContext.EwrApplications
                .Include(req => req.Agency)
                .Where(req => req.CreatedDate >= request.StartDate && req.CreatedDate <= request.EndDate)
                .GroupBy(req => req.Agency.AgencyName)
                .Select(group => new AgencyEwrCountDto
                {
                    UtilityCompany = group.Key,
                    RequestCount = group.Count()
                })
                .AsQueryable();

            return ewrApplications;
        }

        public IQueryable<WardEwrCountDto> GetEwrCountByWard(DateRangeRequest request)
        {
            IQueryable<WardEwrCountDto> ewrApplications = _dbContext.EwrApplications
               .Include(req => req.EwrApplicationLocations).ThenInclude(reqLoc => reqLoc.EwrLocation)
               .Where(req => req.CreatedDate >= request.StartDate && req.CreatedDate <= request.EndDate)
               .SelectMany(req => req.EwrApplicationLocations.Select(reqLoc => reqLoc.EwrLocation))
               .GroupBy(loc => loc.Ward)
               .Select(group => new WardEwrCountDto
               {
                   Ward = group.Key,
                   RequestCount = group.Count()
               })
               .AsQueryable();

            return ewrApplications;
        }

        public IQueryable<EmergencyTypeEwrCountDto> GetEwrCountByEmergencyType(DateRangeRequest request)
        {
            IQueryable<EmergencyTypeEwrCountDto> ewrApplications = _dbContext.EwrApplications
               .Include(req => req.EmergencyType)
               .Where(req => req.CreatedDate >= request.StartDate && req.CreatedDate <= request.EndDate)
               .GroupBy(req => req.EmergencyType.EmergencyTypeDesc)
               .Select(group => new EmergencyTypeEwrCountDto
               {
                   EmergencyType = group.Key,
                   RequestCount = group.Count()
               })
               .AsQueryable();

            return ewrApplications;
        }

        public IQueryable<InspectorCountDto> GetInspectedCountByInspector(DateRangeRequest request)
        {
            IQueryable<InspectorCountDto> inspectionDetails = _dbContext.Users
           .Select(user => new InspectorCountDto
           {
               InspectorName = string.Concat(user.FirstName, " ", user.LastName),
               TotalInspectCount = user.InspDetails
               .Count(req => req.InspectionDate >= request.StartDate && req.InspectionDate <= request.EndDate)
           });

            return inspectionDetails;
        }

        public IQueryable<StatusEwrCountDto> GetEwrCountByStatus(DateRangeRequest request)
        {
            IQueryable<StatusEwrCountDto> ewrApplications = _dbContext.EwrApplications
               .Include(req => req.Status)
               .Where(req => req.CreatedDate >= request.StartDate && req.CreatedDate <= request.EndDate)
               .GroupBy(req => req.Status.StatusDesc)
               .Select(group => new StatusEwrCountDto
               {
                   Status = group.Key,
                   RequestCount = group.Count()
               })
               .AsQueryable();

            return ewrApplications;
        }

        public IQueryable<EwrResponseDto> GetAllByDateRange(DateRangeRequest request)
        {
            IQueryable<EwrApplication> ewrApplications = _dbContext.EwrApplications
                .Include(req => req.EmergencyType)
                .Include(req => req.EmergencyCause)
                .Include(req => req.EmergencyCategory)
                .Include(req => req.AppliedByNavigation)
                .Include(req => req.AssignedInspectorNavigation)
                .Include(req => req.EwrApplicationLocations)
                    .ThenInclude(reqLoc => reqLoc.EwrLocation)
                .Include(req => req.Status)
                .Include(req => req.Tcp)
                .Include(req => req.Agency)
                .Include(req => req.SwoApplication);

            if (request.StartDate != null) ewrApplications = ewrApplications.Where(req => req.CreatedDate >= request.StartDate);
            if (request.EndDate != null) ewrApplications = ewrApplications.Where(req => req.CreatedDate <= request.EndDate);

            IQueryable<EwrResponseDto> finalizedEwrRequests = ewrApplications.OrderByDescending(req => req.SubmissionDate.HasValue ? req.SubmissionDate : req.CreatedDate)
            .Skip((request.PagingAndSortingInfo.Paging.PageNo - 1) * request.PagingAndSortingInfo.Paging.PageSize)
            .Take(request.PagingAndSortingInfo.Paging.PageSize)
            .Select(req => new EwrResponseDto
            {
                RequestId = req.EwrApplicationId,
                RequestNumber = req.EwrRequestNumber,
                Location = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.FullDescription).FirstOrDefault(),
                Ward = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Ward).FirstOrDefault(),
                EffectiveDate = req.EffectiveDate,
                ExpirationDate = req.ExpirationDate,
                EmergencyType = req.EmergencyType.EmergencyTypeDesc,
                EmergencyCause = req.EmergencyCause.EmergencyCauseDesc,
                Status = req.Status.StatusDesc,
                AppliedBy = string.Concat(req.AppliedByNavigation.FirstName, " ", req.AppliedByNavigation.LastName),
                CreationDate = req.CreatedDate,
                UtilityCompany = req.Agency.AgencyName,
                InternalUtilityTrackingNumber = req.ClientReferenceNum,
                AssignedInspector = string.Concat(req.AssignedInspectorNavigation.FirstName, " ", req.AssignedInspectorNavigation.LastName),
                LastInspectionDate = req.LastInspectionDate,
                XCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.XCoord).FirstOrDefault(),
                YCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.YCoord).FirstOrDefault(),
                Latitude = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Latitude).FirstOrDefault(),
                Longitude = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.Longitude).FirstOrDefault(),
                MarXCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.MarXCoord).FirstOrDefault(),
                MarYCoord = req.EwrApplicationLocations.Select(loc => loc.EwrLocation.MarYCoord).FirstOrDefault(),
            }
            )
            .AsQueryable();

            return finalizedEwrRequests;
        }

        public IQueryable<EwrStatusOption> GetAllEwrStatuses()
        {
            IQueryable<EwrStatusOption> ewrStatuses = _dbContext.EwrStatuses
            .Select(res => new EwrStatusOption
            {
                StatusId = res.StatusId,
                StatusDesc = res.StatusDesc,
            }).AsQueryable();

            return ewrStatuses;
        }

        public async Task<EwrApplication> UpdateEwrApplication(EwrApplication ewrApplication)
        {
            _dbContext.Update(ewrApplication);
            await _dbContext.SaveChangesAsync();
            return ewrApplication;
        }

        public async Task<IList<EwrApplication>> UpdateEwrApplications(List<EwrApplication> ewrApplications)
        {
            _dbContext.UpdateRange(ewrApplications);
            await _dbContext.SaveChangesAsync();
            return ewrApplications;
        }


        public IQueryable<EwrEmergencyTypeOption> GetAllEwrEmergencyTypes()
        {
            IQueryable<EwrEmergencyTypeOption> ewrEmergencyTypes = _dbContext.EwrEmergencyTypes
            .Select(res => new EwrEmergencyTypeOption
            {
                EmergencyTypeId = res.EmergencyTypeId,
                EmergencyTypeDesc = res.EmergencyTypeDesc,
            }).AsQueryable();

            return ewrEmergencyTypes;
        }

        public IQueryable<EwrEmergencyCauseOption> GetAllEwrEmergencyCauses()
        {
            IQueryable<EwrEmergencyCauseOption> ewrEmergencyCauses = _dbContext.EwrEmergencyCauses
            .Select(res => new EwrEmergencyCauseOption
            {
                EmergencyCauseId = res.EmergencyCauseId,
                EmergencyCauseDesc = res.EmergencyCauseDesc,
            }).AsQueryable();

            return ewrEmergencyCauses;
        }

        IQueryable<EwrEmergencyCategoryOption> IMpsEwrRepository.GetAllEwrEmergencyCategories()
        {
            IQueryable<EwrEmergencyCategoryOption> ewrEmergencyCategories = _dbContext.EwrEmergencyCategories
            .Select(res => new EwrEmergencyCategoryOption
            {
                EmergencyCategoryId = res.EmergencyCategoryId,
                EmergencyCategoryDesc = res.EmergencyCategory,
            }).AsQueryable();

            return ewrEmergencyCategories;
        }

        public async Task<EwrApplication> CreateEwr(EwrCreateRequest ewrRequest)
        {
            EwrApplication ewrApplication = new EwrApplication()
            {
                EmergencyCategoryId = ewrRequest.EmergencyCategoryId,
                EmergencyCauseId = ewrRequest.EmergencyCauseId,
                EmergencyTypeId = ewrRequest.EmergencyTypeId,
                ProblemDetails = ewrRequest.EmergencyDescription,
                HasRushHourRestrictions = ewrRequest.RushHourRestriction,
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = ewrRequest.CreatedBy,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = ewrRequest.CreatedBy,
                ApplicationDate = DateTime.UtcNow,
                AppliedBy = ewrRequest.CreatedBy,
                StatusId = (int)DDOT.MPS.Permit.Core.Enums.EwrStatus.Submitted,
                EffectiveDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddHours(72),
                StartTime = DateTime.UtcNow.ToString("HH:mm:ss"),
                SubmissionDate = DateTime.UtcNow,
                SubmittedBy = ewrRequest.CreatedBy
            };
            _dbContext.Add(ewrApplication);
            await _dbContext.SaveChangesAsync();

            if (ewrRequest.EwrLocation != null)
            {
                EwrLocation newEwrLocation = _mapper.Map<EwrLocation>(ewrRequest.EwrLocation);
                newEwrLocation.IsActive = true;
                newEwrLocation.CreatedDate = DateTime.UtcNow;
                newEwrLocation.CreatedBy = ewrRequest.CreatedBy;
                newEwrLocation.ModifiedDate = DateTime.UtcNow;
                newEwrLocation.ModifiedBy = ewrRequest.CreatedBy;
                _dbContext.Add(newEwrLocation);
                await _dbContext.SaveChangesAsync();

                EwrApplicationLocation newEwrApplicationLocation = new EwrApplicationLocation()
                {
                    EwrLocationId = newEwrLocation.EwrLocationId,
                    EwrApplicationId = ewrApplication.EwrApplicationId,
                    EwrApplication = ewrApplication,
                    EwrLocation = newEwrLocation,
                    IsActive = true,
                    CreatedBy = ewrRequest.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = ewrRequest.CreatedBy,
                    ModifiedDate = DateTime.UtcNow
                };
                _dbContext.Add(newEwrApplicationLocation);
                await _dbContext.SaveChangesAsync();
            }

            return ewrApplication;

        }

        public List<EwrResponseDto> GetEWRByLocation(EwrPaginatedRequest request)
        {
            IQueryable<EwrResponseDto> ewrApplications = GetAll(request);
            ewrApplications = ewrApplications.OrderByDescending(i => i.EffectiveDate.HasValue ? i.EffectiveDate : i.CreationDate);
            return ewrApplications.ToList();
        }

    }
}
