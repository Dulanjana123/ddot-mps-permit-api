using DDOT.MPS.Permit.Model.Dtos;

namespace DDOT.MPS.Permit.Model.Request
{
    public class EwrCreateRequest
    {
        public int EmergencyCategoryId { get; set; }

        public int EmergencyTypeId { get; set; }

        public int EmergencyCauseId { get; set; }
        public string? EmergencyDescription { get; set; }
        public bool? RushHourRestriction { get; set; }
        public int? CreatedBy { get; set; }
        public EwrLocationDto? EwrLocation { get; set; }

    }
}
