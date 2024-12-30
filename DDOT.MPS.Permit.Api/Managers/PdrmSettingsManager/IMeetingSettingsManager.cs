using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers.PdrmSettingsManager
{
    public interface IMeetingSettingsManager
    {
        public Task<BaseResponse<PdrmMeetingConstraintsDto>> GetAll(int meetingTypeId);

        public Task<BaseResponse<CalendarDatesSelectionReqDto>> CreateOrUpdateCalendarDatesSelectionData(
            CalendarDatesSelectionReqDto calendarDatesSelectionReq);

        public Task<BaseResponse<TimeBlocksSelectionDtoReqDto>> CreateOrUpdateAvailableTimeBlocks(
            TimeBlocksSelectionDtoReqDto calendarDatesSelectionDtoReq);
    }
}
