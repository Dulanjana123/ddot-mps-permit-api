namespace DDOT.MPS.Permit.Model.Response
{
    public class EwrCreateResponse
    {
        public int EwrApplicationId { get; set; }
        public string EwrRequestNumber { get; set; } = null!;
        public int EmergencyCategoryId { get; set; }
        public int? EmergencyTypeId { get; set; }
        public int? EmergencyCauseId { get; set; }
        public string? ProblemDetails { get; set; }
        public bool? HasRushHourRestrictions { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? StartTime { get; set; }
        public int? AppliedBy { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public int? StatusId { get; set; }
        public bool? IsCondition { get; set; }
        public string? LocationDetail { get; set; }
        public string? LocationDescription { get; set; }
        public int? TcpId { get; set; }
        public string? ClientReferenceNum { get; set; }
        public int? AssignedInspector { get; set; }
        public int? CpApplicationId { get; set; }
        public int? OpApplicationId { get; set; }
        public int? NoiApplicationId { get; set; }
        public int? SwoApplicationId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? SubmittedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public int? IssuedBy { get; set; }
        public string? Comments { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? RejectedDate { get; set; }
        public int? RejectedBy { get; set; }
        public DateTime? NotificationSentDate { get; set; }
        public int? InspStatusId { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public int? AgencyId { get; set; }
        public bool IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LegacyIdForMigration { get; set; }
        public bool IsMigratedFromTops { get; set; }
    
    }
}
