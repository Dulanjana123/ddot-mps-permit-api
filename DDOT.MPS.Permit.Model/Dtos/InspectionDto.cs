
namespace DDOT.MPS.Permit.Model.Dtos
{
    public class InspectionDto
    {
        public int ApplicationId { get; set; }

        public string ApplicationTypeCode { get; set; }

        public int InspectedBy { get; set; }

        public int? InspTypeId { get; set; }

        public int? InspStatusId { get; set; }

        public DateTime? InspectionDate { get; set; }

        public int? MinutesSpent { get; set; }

        public string? InternalNotes { get; set; }

        public string? ExternalNotes { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
