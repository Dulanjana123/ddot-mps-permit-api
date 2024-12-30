using DDOT.MPS.Permit.Core.Enums;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class TimeBlocksSelectionDto
    {
        public List<TimeBlockDto> TimeBlocks { get; set; }
    }

    public class TimeBlocksSelectionDtoReqDto : TimeBlocksSelectionDto
    {
        public int MeetingTypeId { get; set; }
        public CommonActions Action { get; set; }
    }
}