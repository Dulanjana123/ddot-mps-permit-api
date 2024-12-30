using DDOT.MPS.Permit.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository
{
    public interface IPdrmSettingsRepository
    {
        public Task<PdrmMeetingConstraintsDto> GetMeetingsSettingsAsync(int meetingTypeId);
        public Task<List<int>> GetMeetingTypeIds();
        public Task<CalendarDatesSelectionReqDto> CreateOrUpdateCalendarDatesSelectionData(CalendarDatesSelectionReqDto calendarDatesSelectionReq);
        public Task<TimeBlocksSelectionDtoReqDto> CreateOrUpdateAvailableTimeBlocks(TimeBlocksSelectionDtoReqDto calendarDatesSelectionDtoReq);
    }
}
