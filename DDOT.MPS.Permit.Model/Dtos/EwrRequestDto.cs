using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class EwrRequestDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public short? EmergencyTypeId { get; set; }

        public short? EmergencyCauseId { get; set; }

        public string? ProblemDetails { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? StartTime { get; set; }

        public int? AppliedBy { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public short? StatusId { get; set; }

        public bool? IsCondition { get; set; }

        public string? LocationDetail { get; set; }

        public string? LocationDescription { get; set; }

        public int? TcpId { get; set; }

        public string? ClientReferenceNum { get; set; }

        public string? AssignedInspector { get; set; }

        public int? ConstructionTrackingId { get; set; }

        public int? OccupancyTrackingId { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public int? SubmittedBy { get; set; }

        public DateTime? IssuedDate { get; set; }

        public int? IssuedBy { get; set; }

        public string? Comments { get; set; }

        public DateTime? CancelledDate { get; set; }

        public int? CancelledBy { get; set; }

        public DateTime? RejectedDate { get; set; }

        public int? RejectedBy { get; set; }

        public int? LastUpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public DateTime? NotificationSentDate { get; set; }

        public short? InspStatusId { get; set; }

        public DateTime? LastInspectionDate { get; set; }

        public int? UserAgenciesCompaniesId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? LegacyIdForMigration { get; set; }
    }
}
