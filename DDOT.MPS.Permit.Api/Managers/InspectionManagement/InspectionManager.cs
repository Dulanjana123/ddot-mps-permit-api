using AutoMapper;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.ApplicationTypeRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers.InspectionManagement
{
    public class InspectionManager : IInspectionManager
    {

        private readonly IMpsApplicationTypeRepository _applicationTypeRepository;
        private readonly IInspectionRepository _inspectionRepository;
        private readonly IMapper _mapper;

        public InspectionManager(IInspectionRepository inspectionRepository, IMpsApplicationTypeRepository applicationTypeRepository, IMapper mapper)
        {
            _inspectionRepository = inspectionRepository;
            _applicationTypeRepository = applicationTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<InspectionResponseDto>> CreateInspection(InspectionDto inspection)
        {

            ApplicationType? applicationType = await _applicationTypeRepository.GetApplicationTypeByTypeCode(inspection.ApplicationTypeCode);
            if (applicationType != null)
            {
                InspDetail mpsInspection = new InspDetail()
                {
                    ApplicationId = inspection.ApplicationId,
                    ApplicationType = applicationType,
                    InspectedBy = inspection.InspectedBy,
                    InspTypeId = inspection.InspTypeId,
                    InspStatusId = inspection.InspStatusId,
                    InspectionDate = inspection.InspectionDate,
                    MinutesSpent = inspection.MinutesSpent,
                    InternalNotes = inspection.InternalNotes,
                    ExternalNotes = inspection.ExternalNotes,
                    CreatedDate = DateTime.UtcNow,
                };
                await _inspectionRepository.CreateInspection(mpsInspection);
                InspectionResponseDto inspectionResponseDto = _mapper.Map<InspectionResponseDto>(mpsInspection);
                return new BaseResponse<InspectionResponseDto> { Success = true, Data = inspectionResponseDto, Message = "INSPECTION_CREATED_SUCCESSFULLY" };
            }
            return new BaseResponse<InspectionResponseDto> { Success = false, Message = "APPLICATION_TYPE_CODE_NOT_FOUND" };

        }

        public async Task<BaseResponse<InspectionResponseDto>> GetInspectionById(int id)
        {
            InspectionResponseDto? mpsInspection = await _inspectionRepository.GetInspectionById(id);
            if (mpsInspection == null)
            {
                throw new UDNotFoundException("INSPECTION_DETAILS_NOT_FOUND");
            }
            return new BaseResponse<InspectionResponseDto> { Success = true, Data = mpsInspection, Message = "INSPECTION_DETAILS_RETRIEVED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<Result<InspectionResponseDto>>> GetPaginatedList(InspectionPaginatedRequest request)
        {
            IQueryable<InspectionResponseDto> inspectionResponseDtos = _inspectionRepository.GetAll(request);

            BaseResponse<Result<InspectionResponseDto>> response = new BaseResponse<Result<InspectionResponseDto>>
            {
                Success = true,
                Data = new Result<InspectionResponseDto>
                {
                    Entities = inspectionResponseDtos.ToArray(),
                    Pagination = new Pagination()
                    {
                        Length = _inspectionRepository.GetRowCount(request),
                        PageSize = request.PagingAndSortingInfo.Paging.PageSize
                    }
                }
            };

            return response;
        }

        public async Task<BaseResponse<InspectionResponseDto>> UpdateInspection(int id, InspectionDto inspection)
        {
            InspDetail inspDetail = await _inspectionRepository.GetById(id);

            if (inspDetail == null)
            {
                throw new UDNotFoundException("INSPECTION_DETAILS_NOT_FOUND");
            }

            inspDetail.InspectedBy = inspection.InspectedBy;
            inspDetail.InspectionDate = inspection.InspectionDate;
            inspDetail.MinutesSpent = inspection.MinutesSpent;
            inspDetail.InternalNotes = inspection.InternalNotes;
            inspDetail.ExternalNotes = inspection.ExternalNotes;
            inspDetail.ModifiedDate = DateTime.UtcNow;

            InspDetail mpsInspection = await _inspectionRepository.UpdateInspection(inspDetail);
            InspectionResponseDto inspectionResponseDto = _mapper.Map<InspectionResponseDto>(mpsInspection);

            return new BaseResponse<InspectionResponseDto> { Success = true, Data = inspectionResponseDto, Message = "INSPECTION_UPDATED_SUCCESSFULLY" };

        }
    }
}
