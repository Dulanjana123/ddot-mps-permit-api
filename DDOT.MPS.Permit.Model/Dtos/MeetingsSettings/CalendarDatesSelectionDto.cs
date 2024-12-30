namespace DDOT.MPS.Permit.Model.Dtos
{
    public class CalendarDatesSelectionBaseDto
    {
        public List<KeyValuePair<string, bool>> ScheduledDays { get; set; }
    }
    public class CalendarDatesSelectionDto : CalendarDatesSelectionBaseDto
    {
        public List<KeyValuePair<int, string>> MeetingTypes { get; set; }
    }

    public class CalendarDatesSelectionReqDto : CalendarDatesSelectionBaseDto
    {
        public int MeetingTypeId { get; set; }
    }
}