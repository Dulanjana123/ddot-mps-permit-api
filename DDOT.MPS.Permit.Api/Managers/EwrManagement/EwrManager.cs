using AutoMapper;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers
{
    public class EwrManager : IEwrManager
    {
        private readonly IMapper _mapper;
        private readonly IMpsEwrRepository _ewrRepository;
        private readonly ISwoRepository _swoRepository;
        private readonly IMpsUserRepository _userRepository;
        private readonly IInspectionRepository _inspectionRepository;

        public EwrManager(IMpsEwrRepository ewrRepository, ISwoRepository swoRepository, IMpsUserRepository userRepository, IInspectionRepository inspectionRepository, IMapper mapper)
        {
            _ewrRepository = ewrRepository;
            _swoRepository = swoRepository;
            _userRepository = userRepository;
            _inspectionRepository = inspectionRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Result<EwrResponseDto>>> GetPaginatedList(EwrPaginatedRequest request)
        {

            IQueryable<EwrResponseDto> ewrResponseDtos = _ewrRepository.GetAll(request);

            List<EwrResponseDto> ewrResponseList = await ewrResponseDtos.ToListAsync();

            BaseResponse<Result<EwrResponseDto>> response = new BaseResponse<Result<EwrResponseDto>>
            {
                Success = true,
                Data = new Result<EwrResponseDto>
                {
                    Entities = ewrResponseList.ToArray(),
                    Pagination = new Pagination()
                    {
                        Length = _ewrRepository.GetRowCount(request),
                        PageSize = request.PagingAndSortingInfo.Paging.PageSize
                    }
                }
            };

            return response;
        }

        public async Task<BaseResponse<EwrResponseDto>> GetById(int id)
        {
            EwrApplication mpsEwrApplication = await _ewrRepository.GetById(id);

            if (mpsEwrApplication == null)
            {
                throw new UDNotFoundException("EWR_REQUEST_NOT_FOUND");
            }

            // This mapping sould be optimized after requirenment finalized.
            EwrResponseDto ewrResponseDto = new EwrResponseDto
            {
                RequestId = mpsEwrApplication.EwrApplicationId,
                RequestNumber = mpsEwrApplication.EwrRequestNumber,
                EffectiveDate = mpsEwrApplication?.EffectiveDate,
                ExpirationDate = mpsEwrApplication?.ExpirationDate,
                IsCondition = mpsEwrApplication?.IsCondition,
                EmergencyType = mpsEwrApplication?.EmergencyType?.EmergencyTypeDesc,
                EmergencyCause = mpsEwrApplication?.EmergencyCause?.EmergencyCauseDesc,
                Status = mpsEwrApplication?.Status?.StatusDesc,
                AppliedBy = mpsEwrApplication?.AppliedBy != null ? string.Concat(mpsEwrApplication?.AppliedByNavigation.FirstName, " ", mpsEwrApplication?.AppliedByNavigation.LastName) : "",
                CreationDate = mpsEwrApplication?.CreatedDate,
                UtilityCompany = mpsEwrApplication?.Agency?.AgencyName,
                InternalUtilityTrackingNumber = mpsEwrApplication?.ClientReferenceNum,
                AssignedInspector = (mpsEwrApplication?.AssignedInspector) != null ? string.Concat(mpsEwrApplication?.AssignedInspectorNavigation.FirstName, " ", mpsEwrApplication?.AssignedInspectorNavigation.LastName) : "",
                LastInspectionDate = mpsEwrApplication?.LastInspectionDate,
                TrafficControlPlan = mpsEwrApplication?.Tcp?.TcpName,
                ProblemDetails = mpsEwrApplication?.ProblemDetails,
                Ward = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.Ward).FirstOrDefault(),
                Location = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc.EwrLocation?.FullDescription).FirstOrDefault(),
                XCoord = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.XCoord).FirstOrDefault(),
                YCoord = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.YCoord).FirstOrDefault(),
                Latitude = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc.EwrLocation.Latitude).FirstOrDefault(),
                Longitude = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc.EwrLocation.Longitude).FirstOrDefault(),
                MarXCoord = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.MarXCoord).FirstOrDefault(),
                MarYCoord = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.MarYCoord).FirstOrDefault(),
                LocationId = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc.EwrLocationId).FirstOrDefault(),
                IsPsAddConstructionWork = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.IsPsToAddConsWork).FirstOrDefault(),
                HasRushHourRestriction = mpsEwrApplication?.HasRushHourRestrictions,
                AddressType = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.LocationType.LocationTypeName).FirstOrDefault(),
                LocationCategory = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.LocationCategory.LocationCategoryName).FirstOrDefault(),
                Quadrant = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.Quadrant).FirstOrDefault(),
                Lot = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.ALot).FirstOrDefault(),
                Square = mpsEwrApplication?.EwrApplicationLocations.Select(loc => loc?.EwrLocation?.ASquare).FirstOrDefault(),
                CpApplicationId = mpsEwrApplication?.CpApplicationId,
                NoiApplicationId = mpsEwrApplication?.NoiApplicationId,
                SwoApplicationId = mpsEwrApplication?.SwoApplicationId
            };

            return new BaseResponse<EwrResponseDto> { Success = true, Data = ewrResponseDto, Message = "EWR_REQUEST_FETCHED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<EwrDashboardResponseDto>> GetEwrDashboardData(DateRangeRequest request)
        {
            long totalEwrCount = _ewrRepository.GetTotalEwrCount(request);
            long ewrCountWithoutCP = _ewrRepository.EwrCountWithoutCP(request);

            IQueryable<AgencyEwrCountDto> agencyEwrCounts = _ewrRepository.GetEwrCountByUtilityCompany(request);
            List<AgencyEwrCountDto> agencyEwrList = agencyEwrCounts.ToList();

            IQueryable<WardEwrCountDto> wardEwrCounts = _ewrRepository.GetEwrCountByWard(request);
            List<WardEwrCountDto> wardEwrList = wardEwrCounts.ToList();

            IQueryable<EmergencyTypeEwrCountDto> emergencyTypeEwrCounts = _ewrRepository.GetEwrCountByEmergencyType(request);
            List<EmergencyTypeEwrCountDto> emergencyTypeEwrList = emergencyTypeEwrCounts.ToList();

            IQueryable<InspectorCountDto> inspectorHoursCounts = _ewrRepository.GetInspectedCountByInspector(request);
            List<InspectorCountDto> inspectorHoursList = inspectorHoursCounts.ToList();

            IQueryable<StatusEwrCountDto> statusEwrCounts = _ewrRepository.GetEwrCountByStatus(request);
            List<StatusEwrCountDto> statusEwrList = statusEwrCounts.ToList();

            IQueryable<EwrResponseDto> ewrResponseDtos = _ewrRepository.GetAllByDateRange(request);
            List<EwrResponseDto> ewrList = ewrResponseDtos.ToList();

            EwrDashboardResponseDto ewrDashboardResponseDto = new EwrDashboardResponseDto()
            {
                TotalEwrCount = totalEwrCount,
                EwrCountWithoutCP =ewrCountWithoutCP,
                EwrVsAgencyChartData = agencyEwrList,
                WardVsEwrChartData = wardEwrList,
                EmergencyTypeVsEwrChartData = emergencyTypeEwrList,
                StatusVsEwrChartData = statusEwrList,
                InspectorCountChartData = inspectorHoursList,
                EwrData = ewrList
            };

            return new BaseResponse<EwrDashboardResponseDto> { Success = true, Data = ewrDashboardResponseDto, Message = "EWR_DASHBOARD_DATA_RETRIEVED" };

        }

        public async Task<BaseResponse<AssigningInfoResponseDto>> GetAssigningInfo()
        {
            IQueryable<UserOption> inspectors = _userRepository.GetAllInspectors();
            List<UserOption> inspectorsList = inspectors.ToList();

            IQueryable<InspStatusOption> inspStatuses = _inspectionRepository.GetAllInspectionStatuses();
            List<InspStatusOption> inspStatusesList = inspStatuses.ToList();

            IQueryable<EwrStatusOption> ewrStatuses = _ewrRepository.GetAllEwrStatuses();
            List<EwrStatusOption> ewrStatusesList = ewrStatuses.ToList();

            AssigningInfoResponseDto assigningInfoDto = new AssigningInfoResponseDto()
            {
                Inspectors = inspectorsList,
                InspStatuses = inspStatusesList,
                EwrStatuses = ewrStatusesList,
            };

            return new BaseResponse<AssigningInfoResponseDto> { Success = true, Data = assigningInfoDto, Message = "ASSIGNING_INFO_RETRIEVED" };

        }

        public async Task<BaseResponse<EwrAssigningDto>> UpdateEwrAssigning(int id, EwrAssigningDto ewrAssigningDto)
        {
            EwrApplication mpsEwrApplication = await _ewrRepository.GetById(id);
            if (mpsEwrApplication == null)
            {
                throw new UDNotFoundException("EWR_NOT_FOUND");
            }

            if (ewrAssigningDto.AssigneeId.HasValue && ewrAssigningDto.AssigneeId != 0) mpsEwrApplication.AssignedInspector = ewrAssigningDto.AssigneeId;
            if (ewrAssigningDto.EwrStatusId.HasValue && ewrAssigningDto.EwrStatusId != 0) mpsEwrApplication.StatusId = ewrAssigningDto.EwrStatusId;

            InspDetail inspectionDetail = new InspDetail()
            {
                ApplicationId = mpsEwrApplication.EwrApplicationId,
                ApplicationTypeId = 7, // By default 7 for EWR Applications
                ApplicationStatusId = ewrAssigningDto.EwrStatusId.HasValue ? ewrAssigningDto.EwrStatusId : mpsEwrApplication.StatusId,
                InspectedBy = (int)(ewrAssigningDto.AssigneeId.HasValue ? ewrAssigningDto.AssigneeId : mpsEwrApplication.AssignedInspector),
                InspStatusId = ewrAssigningDto.InspStatusId,
                Comments = ewrAssigningDto.Comments,
                CreatedDate = DateTime.UtcNow,
            };

            await _inspectionRepository.CreateInspection(inspectionDetail);
            await _ewrRepository.UpdateEwrApplication(mpsEwrApplication);

            return new BaseResponse<EwrAssigningDto> { Success = true, Data = ewrAssigningDto, Message = "EWR_ASSIGNED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<EwrBulkAssigningDto>> UpdateEwrBulkAssigning(EwrBulkAssigningDto ewrBulkAssigningDto)
        {

            List<EwrApplication> ewrApplications = await _ewrRepository.GetByIdList(ewrBulkAssigningDto.EwrApplicationIds);
            List<InspDetail> inspDetailList = new List<InspDetail>();

            foreach (EwrApplication ewrApplication in ewrApplications)
            {
                if (ewrBulkAssigningDto.AssigneeId.HasValue && ewrBulkAssigningDto.AssigneeId != 0) ewrApplication.AssignedInspector = ewrBulkAssigningDto.AssigneeId;
                if (ewrBulkAssigningDto.EwrStatusId.HasValue && ewrBulkAssigningDto.EwrStatusId != 0) ewrApplication.StatusId = ewrBulkAssigningDto.EwrStatusId;

                inspDetailList.Add(new InspDetail
                {
                    ApplicationId = ewrApplication.EwrApplicationId,
                    ApplicationTypeId = 7, // Default to 7 for EWR Applications
                    ApplicationStatusId = ewrBulkAssigningDto.EwrStatusId ?? ewrApplication.StatusId,
                    InspectedBy = (int)(ewrBulkAssigningDto.AssigneeId.HasValue ? ewrBulkAssigningDto.AssigneeId : ewrApplication.AssignedInspector),
                    InspStatusId = ewrBulkAssigningDto.InspStatusId,
                    Comments = ewrBulkAssigningDto.Comments,
                    CreatedDate = DateTime.UtcNow,
                });
            }

            await _ewrRepository.UpdateEwrApplications(ewrApplications);
            await _inspectionRepository.CreateBulk(inspDetailList);

            return new BaseResponse<EwrBulkAssigningDto> { Success = true, Data = ewrBulkAssigningDto, Message = "EWRS_BULK_ASSIGNED_SUCCESSFULLY" };

        }

        public async Task<BaseResponse<EwrIndexFiltersInfoResponseDto>> GetIndexFiltersInfo()
        {
            IQueryable<EwrStatusOption> ewrStatuses = _ewrRepository.GetAllEwrStatuses();
            List<EwrStatusOption> ewrStatusesList = ewrStatuses.ToList();

            IQueryable<EwrEmergencyTypeOption> ewrEmergencyTypes = _ewrRepository.GetAllEwrEmergencyTypes();
            List<EwrEmergencyTypeOption> ewrEmergencyTypesList = ewrEmergencyTypes.ToList();

            IQueryable<EwrEmergencyCauseOption> ewrEmergencyCauses = _ewrRepository.GetAllEwrEmergencyCauses();
            List<EwrEmergencyCauseOption> ewrEmergencyCausesList = ewrEmergencyCauses.ToList();

            IQueryable<EwrEmergencyCategoryOption> ewrEmergencyCategories = _ewrRepository.GetAllEwrEmergencyCategories();
            List<EwrEmergencyCategoryOption> ewrEmergencyCategoriesList = ewrEmergencyCategories.ToList();

            IQueryable<UserOption> users = _userRepository.GetAllUsersNames();
            List<UserOption> usersList = users.ToList();

            IQueryable<SwoStatusOption> swoStatuses = _swoRepository.GetAllSwoStatuses();
            List<SwoStatusOption> swoStatusesList = swoStatuses.ToList();

            EwrIndexFiltersInfoResponseDto indexFiltersInfoDto = new EwrIndexFiltersInfoResponseDto()
            {
                EwrStatuses = ewrStatusesList,
                EwrEmergencyTypes = ewrEmergencyTypesList,
                EwrEmergencyCauses = ewrEmergencyCausesList,
                EwrEmergencyCategories = ewrEmergencyCategoriesList,
                Users = usersList,
                SwoStatuses = swoStatusesList,
            };

            return new BaseResponse<EwrIndexFiltersInfoResponseDto> { Success = true, Data = indexFiltersInfoDto, Message = "EWR_INDEX_FILTERS_INFO_RETRIEVED" };
        }

        public async Task<BaseResponse<EwrCreateResponse>> CreateEwr(EwrCreateRequest ewrRequest)
        {
            if (ewrRequest == null)
            {
                throw new UDValiationException("INVALID_REQUEST");
            }
            else if (ewrRequest.EmergencyCategoryId == 0)
            {
                throw new UDValiationException("INVALID_EMERGENCY_CATEGORY");
            }
            else if (ewrRequest.EmergencyCauseId == 0)
            {
                throw new UDValiationException("INVALID_EMERGENCY_CAUSE");
            }
            else if (ewrRequest.EmergencyTypeId == 0)
            {
                throw new UDValiationException("INVALID_EMERGENCY_TYPE");
            }

            EwrApplication ewrApplication = await _ewrRepository.CreateEwr(ewrRequest);
            EwrCreateResponse ewrResponseDto = _mapper.Map<EwrCreateResponse>(ewrApplication);

            return new BaseResponse<EwrCreateResponse> { Success = true, Data = ewrResponseDto, Message = "EWR_CREATED_SUCCESSFULLY" };
        }


        public async Task<BaseResponse<EwrCloseDto>> CloseEwr(int id, EwrCloseDto ewrCloseDto)
        {
            EwrApplication mpsEwrApplication = await _ewrRepository.GetById(id);
            if (mpsEwrApplication == null)
            {
                throw new UDNotFoundException("EWR_NOT_FOUND");
            }

            mpsEwrApplication.ReasonForClose = ewrCloseDto.ReasonForClose;
            mpsEwrApplication.CancelledDate = ewrCloseDto.CancelledDate;
            if (ewrCloseDto.CancelledBy.HasValue) mpsEwrApplication.CancelledBy = ewrCloseDto.CancelledBy;
            // Cancelled Status Id = 1
            mpsEwrApplication.StatusId = 1;

            await _ewrRepository.UpdateEwrApplication(mpsEwrApplication);

            return new BaseResponse<EwrCloseDto> { Success = true, Data = ewrCloseDto, Message = "EWR_CLOSED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<Result<EwrResponseDto>>> GetEWRByLocation(EwrLocationFilterRequest locationFilterRequest)
        {
            EwrPaginatedRequest paginatedRequest = new EwrPaginatedRequest
            {
                Filters = new EwrPaginatedFilters
                {
                    LocationId = locationFilterRequest.LocationId
                },
                PagingAndSortingInfo = locationFilterRequest.PagingAndSortingInfo
            };

            List<EwrResponseDto> ewrResponseDtos = _ewrRepository.GetEWRByLocation(paginatedRequest);
            if (ewrResponseDtos == null)
            {
                throw new UDNotFoundException("DATA_NOT_FOUND");
            }
            BaseResponse<Result<EwrResponseDto>> response = new BaseResponse<Result<EwrResponseDto>>
            {
                Success = true,
                Data = new Result<EwrResponseDto>
                {
                    Entities = ewrResponseDtos.ToArray(),
                    Pagination = new Pagination()
                    {
                        Length = ewrResponseDtos.Count,
                        PageSize = locationFilterRequest.PagingAndSortingInfo.Paging.PageSize
                    }
                }
            };

            return response;
        }
    }
}
