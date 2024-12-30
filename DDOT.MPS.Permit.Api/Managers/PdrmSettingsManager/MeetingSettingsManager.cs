using AutoMapper;
using DDOT.MPS.Permit.Core.Enums;
using DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers.PdrmSettingsManager
{
    public class MeetingSettingsManager : IMeetingSettingsManager
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingSettingsManager> _logger;
        private readonly IPdrmSettingsRepository _pdrmSettingsRepository;
        public MeetingSettingsManager(IMapper mapper, ILogger<MeetingSettingsManager> logger, IPdrmSettingsRepository pdrmSettingsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _pdrmSettingsRepository = pdrmSettingsRepository;
        }

        public async Task<BaseResponse<PdrmMeetingConstraintsDto>> GetAll(int meetingTypeId)
        {
            List<int> meetingTypeIds = await _pdrmSettingsRepository.GetMeetingTypeIds();

            if (!meetingTypeIds.Contains(meetingTypeId))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.GetAll | INVALID_MEETING_TYPE_ID");
                return new BaseResponse<PdrmMeetingConstraintsDto>
                {
                    Data = null,
                    Message = "INVALID_MEETING_TYPE_ID",
                    Success = false
                };
            }

            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.GetAll | Request in progress");

            PdrmMeetingConstraintsDto meetingSettingsDto = await _pdrmSettingsRepository.GetMeetingsSettingsAsync(meetingTypeId);

            return new BaseResponse<PdrmMeetingConstraintsDto>
            {
                Data = meetingSettingsDto,
                Message = "PDRM_SETTINGS_RETRIEVED_SUCCESSFULLY",
                Success = true
            };
        }

        public async Task<BaseResponse<CalendarDatesSelectionReqDto>> CreateOrUpdateCalendarDatesSelectionData(
            CalendarDatesSelectionReqDto calendarDatesSelectionReq)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.CreateOrUpdateCalendarDatesSelectionData | Request in progress");

            foreach (KeyValuePair<string, bool> day in calendarDatesSelectionReq.ScheduledDays)
            {
                if (!Enum.IsDefined(typeof(DayOfWeek), day.Key))
                {
                    _logger.LogError(
                        "DDOT.MPS.Permit.Api.Managers.CreateOrUpdateCalendarDatesSelectionData | INVALID_DAY_OF_WEEK");
                    return new BaseResponse<CalendarDatesSelectionReqDto>
                    {
                        Data = null,
                        Message = "INVALID_DAY_OF_WEEK",
                        Success = false
                    };
                }
            }

            CalendarDatesSelectionReqDto calendarDatesSelectionReqResult = await _pdrmSettingsRepository.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReq);

            if (calendarDatesSelectionReqResult == null)
            {
                _logger.LogError(
                    "DDOT.MPS.Permit.Api.Managers.CreateOrUpdateCalendarDatesSelectionData | DAY_NOT_IN_DATABASE");
                return new BaseResponse<CalendarDatesSelectionReqDto>
                {
                    Data = null,
                    Message = "DAY_NOT_IN_DATABASE",
                    Success = false
                };
            }

            return new BaseResponse<CalendarDatesSelectionReqDto>
            {
                Data = calendarDatesSelectionReqResult,
                Message = "PDRM_SETTINGS_UPDATED_SUCCESSFULLY",
                Success = true
            };
        }


        public async Task<BaseResponse<TimeBlocksSelectionDtoReqDto>> CreateOrUpdateAvailableTimeBlocks(
            TimeBlocksSelectionDtoReqDto timeBlocksSelectionDtoReq)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.CreateOrUpdateAvailableTimeBlocks | Request in progress");

            if (timeBlocksSelectionDtoReq.Action == null || Enum.IsDefined(typeof(CommonActions), timeBlocksSelectionDtoReq.Action) == false)
            {
                return new BaseResponse<TimeBlocksSelectionDtoReqDto>
                {
                    Data = null,
                    Message = "ACTION_REQUIRED",
                    Success = false
                };
            }

            foreach (TimeBlockDto timeBlock in timeBlocksSelectionDtoReq.TimeBlocks)
            {
                if (timeBlock.StartTime >= timeBlock.EndTime)
                {
                    _logger.LogError(
                        "DDOT.MPS.Permit.Api.Managers.CreateOrUpdateAvailableTimeBlocks | START_TIME_MUST_BE_LESS");
                    return new BaseResponse<TimeBlocksSelectionDtoReqDto>
                    {
                        Data = null,
                        Message = "START_TIME_MUST_BE_LESS_THAN_END_TIME",
                        Success = false
                    };
                }

                if (timeBlocksSelectionDtoReq.Action == CommonActions.Add)
                {
                    for (int i = 0; i < timeBlocksSelectionDtoReq.TimeBlocks.Count; i++)
                    {
                        for (int j = i + 1; j < timeBlocksSelectionDtoReq.TimeBlocks.Count; j++)
                        {
                            if (timeBlocksSelectionDtoReq.TimeBlocks[i].StartTime >= timeBlocksSelectionDtoReq.TimeBlocks[j].StartTime &&
                                timeBlocksSelectionDtoReq.TimeBlocks[i].StartTime <= timeBlocksSelectionDtoReq.TimeBlocks[j].EndTime)
                            {
                                _logger.LogError(
                                    "DDOT.MPS.Permit.Api.Managers.CreateOrUpdateAvailableTimeBlocks | TIME_BLOCK_OVERLAP");
                                return new BaseResponse<TimeBlocksSelectionDtoReqDto>
                                {
                                    Data = null,
                                    Message = "TIME_BLOCK_OVERLAP",
                                    Success = false
                                };
                            }
                        }
                    }
                }
            }

            TimeBlocksSelectionDtoReqDto timeBlocksSelectionDtoReqResult = await _pdrmSettingsRepository.CreateOrUpdateAvailableTimeBlocks(timeBlocksSelectionDtoReq);

            return new BaseResponse<TimeBlocksSelectionDtoReqDto>
            {
                Data = timeBlocksSelectionDtoReqResult,
                Message = "PDRM_SETTINGS_UPDATED_SUCCESSFULLY",
                Success = true
            };
        }

    }
}
