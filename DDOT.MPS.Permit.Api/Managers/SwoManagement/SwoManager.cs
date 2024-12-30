using AutoMapper;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers.SwoManagement
{
    public class SwoManager : ISwoManager
    {

        private readonly IMapper _mapper;
        private readonly ISwoRepository _swoRepository;

        public SwoManager(ISwoRepository swoRepository, IMapper mapper)
        {
            _swoRepository = swoRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<SwoResponseDto>> CreateSwoViolation(SwoViolationDto swoViolationDto)
        {
            SwoApplication swoApplicationBySwoNumber = await _swoRepository.GetSwoApplicationBySwoNumber(swoViolationDto.SwoNumber);
            NoteType noteType = await _swoRepository.GetNoteTypeByNoteCode(swoViolationDto.NoteCode);

            if (swoApplicationBySwoNumber != null)
            {
                return new BaseResponse<SwoResponseDto> { Success = false, Data = null, Message = "SWO_NUMBER_ALREADY_EXCIST" };
            }

            if (noteType == null)
            {
                return new BaseResponse<SwoResponseDto> { Success = false, Data = null, Message = "NOTE_TYPE_IS_UNAVAILABLE" };
            }

            SwoApplication swoApplication = new SwoApplication()
            {
                SwoNumber = swoViolationDto.SwoNumber,
                SwoTypeId = swoViolationDto.SwoTypeId,
                SwoStatusId = swoViolationDto.SwoStatusId,
                ViolatedContrName = swoViolationDto.ViolatedContrName,
                ViolatedContrRegNo = swoViolationDto.ViolatedContrRegNo,
                ViolatedContrRegAddr = swoViolationDto.ViolatedContrRegAddr,
                ViolatedOwnerFname = swoViolationDto.ViolatedOwnerFname,
                ViolatedOwnerLname = swoViolationDto.ViolatedOwnerLname,
                ViolationComments = swoViolationDto.ViolationComments,
                IssuedBy = swoViolationDto.IssuedBy,
                IssuedDate = swoViolationDto.IssuedDate,
                IssuedTime = swoViolationDto.IssuedTime,
                WorkSiteForeman = swoViolationDto.WorkSiteForeman,
                WorkSiteForemanPhone = swoViolationDto.WorkSiteForemanPhone,
                WeatherConditions = swoViolationDto.WeatherConditions,
                WorkSiteConditions = swoViolationDto.WorkSiteConditions,
                CreatedBy = swoViolationDto.CreatedBy,
                CreatedDate = swoViolationDto.CreatedDate,
                ModifiedBy = swoViolationDto.ModifiedBy,
                ModifiedDate = swoViolationDto.ModifiedDate,
            };

            SwoApplication createdSwo = await _swoRepository.CreateSwo(swoApplication);

            SwoNote swoNote = new SwoNote()
            {
                SwoApplicationId = createdSwo.SwoApplicationId,
                NoteTypeId = noteType.NoteTypeId,
                Notes = swoViolationDto.InternalNotes,
                CreatedBy = swoViolationDto.CreatedBy,
                CreatedDate = swoViolationDto.CreatedDate,
                ModifiedBy = swoViolationDto.ModifiedBy,
                ModifiedDate = swoViolationDto.ModifiedDate,
            };

            SwoViolation swoViolation = new SwoViolation()
            {
                SwoApplicationId = createdSwo.SwoApplicationId,
                SwoViolationTypeId = swoViolationDto.SwoViolationTypeId,
                CreatedBy = swoViolationDto.CreatedBy,
                CreatedDate = swoViolationDto.CreatedDate,
                ModifiedBy = swoViolationDto.ModifiedBy,
                ModifiedDate = swoViolationDto.ModifiedDate,
            };

            await _swoRepository.CreateSwoNote(swoNote);
            await _swoRepository.CreateSwoViolation(swoViolation);
            SwoResponseDto swoResponseDto = _mapper.Map<SwoResponseDto>(createdSwo);

            return new BaseResponse<SwoResponseDto> { Success = true, Data = swoResponseDto, Message = "SWO_SAVED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<SwoViolationTypesResponseDto>> GetViolationTypes()
        {
            IQueryable<ViolationTypeOption> violationTypes = _swoRepository.GetViolationTypes();
            List<ViolationTypeOption> violationTypesList = violationTypes.ToList();

            SwoViolationTypesResponseDto violationTypesResponseDto = new SwoViolationTypesResponseDto()
            {
                ViolationTypes = violationTypesList,
            };

            return new BaseResponse<SwoViolationTypesResponseDto> { Success = true, Data = violationTypesResponseDto, Message = "VIOLATION_TYPES_RETRIEVED" };
        }

        public async Task<BaseResponse<SwoResponseDto>> UpdateSwoViolation(int id, SwoViolationDto swoViolationDto)
        {
            SwoApplication swoApplication = await _swoRepository.GetById(id);
            if (swoApplication == null)
            {
                throw new UDNotFoundException("SWO_NOT_FOUND");
            }

            SwoViolation swoViolation = await _swoRepository.GetViolationBySwoApplicationId(swoApplication.SwoApplicationId);
            SwoNote swoNote = await _swoRepository.GetSwoNoteBySwoApplicationId(swoApplication.SwoApplicationId);

            // Update SWO Application Table
            swoApplication.SwoTypeId = swoViolationDto.SwoTypeId;
            swoApplication.SwoStatusId = swoViolationDto.SwoStatusId;
            swoApplication.ViolatedContrName = swoViolationDto.ViolatedContrName;
            swoApplication.ViolatedContrRegNo = swoViolationDto.ViolatedContrRegNo;
            swoApplication.ViolatedContrRegAddr = swoViolationDto.ViolatedContrRegAddr;
            swoApplication.ViolatedOwnerFname = swoViolationDto.ViolatedOwnerFname;
            swoApplication.ViolatedOwnerLname = swoViolationDto.ViolatedOwnerLname;
            swoApplication.ViolationComments = swoViolationDto.ViolationComments;
            swoApplication.IssuedBy = swoViolationDto.IssuedBy;
            swoApplication.IssuedDate = swoViolationDto.IssuedDate;
            swoApplication.IssuedTime = swoViolationDto.IssuedTime;
            swoApplication.WorkSiteForeman = swoViolationDto.WorkSiteForeman;
            swoApplication.WorkSiteForemanPhone = swoViolationDto.WorkSiteForemanPhone;
            swoApplication.WeatherConditions = swoViolationDto.WeatherConditions;
            swoApplication.WorkSiteConditions = swoViolationDto.WorkSiteConditions;
            swoApplication.ModifiedBy = swoViolationDto.ModifiedBy;
            swoApplication.ModifiedDate = swoViolationDto.ModifiedDate;

            // Update SWO Violation Table
            swoViolation.SwoViolationTypeId = swoViolationDto.SwoViolationTypeId;
            swoViolation.ModifiedBy = swoViolationDto.ModifiedBy;
            swoViolation.ModifiedDate = swoViolationDto.ModifiedDate;

            // Update SWO Note Table
            swoNote.Notes = swoViolationDto.InternalNotes;
            swoNote.ModifiedBy = swoViolationDto.ModifiedBy;
            swoNote.ModifiedDate = swoViolationDto.ModifiedDate;

            SwoApplication updatedSwo = await _swoRepository.UpdateSwo(swoApplication, swoViolation, swoNote);
            SwoResponseDto swoResponseDto = _mapper.Map<SwoResponseDto>(updatedSwo);

            return new BaseResponse<SwoResponseDto> { Success = true, Data = swoResponseDto, Message = "SWO_UPDATED_SUCCESSFULLY" };

        }
    }
}
