using DDOT.MPS.Permit.Api.Managers.PdrmSettingsManager;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class MeetingSettingsController : CoreController
    {
        private readonly IMeetingSettingsManager _iMeetingSettingsManager;
        private readonly ILogger<MeetingSettingsController> _logger;

        public MeetingSettingsController(IMeetingSettingsManager iMeetingSettingsManager, ILogger<MeetingSettingsController> logger)
        {
            _iMeetingSettingsManager = iMeetingSettingsManager;
            _logger = logger;
        }

        [HttpGet("{meetingTypeId:int}")]
        public async Task<ActionResult<BaseResponse<PdrmMeetingConstraintsDto>>> GetAll(int meetingTypeId)
        {
            _logger.LogInformation(
                "DDOT.MPS.Permit.MeetingSettingsController.GetAll | Request in progress | meetingTypeId: {meetingTypeId}",
                meetingTypeId);
            return Ok(await _iMeetingSettingsManager.GetAll(meetingTypeId));
        }

        [HttpPost("calendar-dates-selection")]
        public async Task<ActionResult<BaseResponse<CalendarDatesSelectionReqDto>>>
            CreateOrUpdateCalendarDatesSelectionData([FromBody] CalendarDatesSelectionReqDto meetingsSettingsReqDto)
        {
            _logger.LogInformation(
                "DDOT.MPS.Permit.MeetingSettingsController.CreateOrUpdateCalendarDatesSelectionData | Request in progress");
            return Ok(await _iMeetingSettingsManager.CreateOrUpdateCalendarDatesSelectionData(meetingsSettingsReqDto));
        }

        [HttpPost("available-time-blocks")]
        public async Task<ActionResult<BaseResponse<TimeBlocksSelectionDtoReqDto>>> CreateOrUpdateAvailableTimeBlocks(
            [FromBody] TimeBlocksSelectionDtoReqDto meetingsSettingsDtoReqDto)
        {
            _logger.LogInformation(
                "DDOT.MPS.Permit.MeetingSettingsController.CreateOrUpdateAvailableTimeBlocks | Request in progress");
            return Ok(await _iMeetingSettingsManager.CreateOrUpdateAvailableTimeBlocks(meetingsSettingsDtoReqDto));
        }
            
    }
}
