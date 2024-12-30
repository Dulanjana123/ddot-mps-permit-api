

namespace DDOT.MPS.Permit.Model.Response
{
    public class EwrResponseDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public string? Location { get; set; }

        public string? Ward { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? EmergencyType { get; set; }

        public string? EmergencyCause { get; set; } 

        public Boolean? IsCondition { get; set; }

        public string? Status { get; set; }

        public string? AppliedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? UtilityCompany { get; set; }

        public string? InternalUtilityTrackingNumber { get; set; }

        public string? AssignedInspector { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public int? ConstructionPermitNumberIfFiled { get; set; }

        public string? TrafficControlPlan { get; set; }

        public string? ProblemDetails { get; set; }

        public string? XCoord { get; set; }

        public string? YCoord { get; set; }
        public Double? Latitude { get; set; }

        public Double? Longitude { get; set; }

        public string? MarXCoord { get; set; }

        public string? MarYCoord { get; set; }

        public int? LocationId { get; set; }
        public Boolean? IsPsAddConstructionWork { get; set; }
        public Boolean? HasRushHourRestriction { get; set; }
        public string? AddressType { get; set; }
        public string? LocationCategory  { get; set; }
        public string? Quadrant { get; set; }
        public string? Lot { get; set; }
        public string? Square { get; set; }

        public int? CpApplicationId { get; set; }

        public int? NoiApplicationId { get; set; }

        public int? SwoApplicationId { get; set; }
    }

}
