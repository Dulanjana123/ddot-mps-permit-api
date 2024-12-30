using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_application")]
[Index("EwrApplicationId", Name = "IX_ewr_application_id")]
[Index("EwrRequestNumber", Name = "UK_ewr_request_number", IsUnique = true)]
public partial class EwrApplication
{
    [Key]
    [Column("ewr_application_id")]
    public int EwrApplicationId { get; set; }

    [Column("ewr_request_number")]
    [StringLength(51)]
    [Unicode(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // This is added manually to mapped with computed column 'EwrRequestNumber'
    public string EwrRequestNumber { get; set; } = null!;

    [Column("emergency_category_id")]
    public int EmergencyCategoryId { get; set; }

    [Column("emergency_type_id")]
    public int? EmergencyTypeId { get; set; }

    [Column("emergency_cause_id")]
    public int? EmergencyCauseId { get; set; }

    [Column("problem_details")]
    [StringLength(4000)]
    [Unicode(false)]
    public string? ProblemDetails { get; set; }

    [Column("has_rush_hour_restrictions")]
    public bool? HasRushHourRestrictions { get; set; }

    [Column("effective_date", TypeName = "datetime")]
    public DateTime? EffectiveDate { get; set; }

    [Column("expiration_date", TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [Column("start_time")]
    [StringLength(10)]
    [Unicode(false)]
    public string? StartTime { get; set; }

    [Column("applied_by")]
    public int? AppliedBy { get; set; }

    [Column("application_date", TypeName = "datetime")]
    public DateTime? ApplicationDate { get; set; }

    [Column("status_id")]
    public int? StatusId { get; set; }

    [Column("is_condition")]
    public bool? IsCondition { get; set; }

    [Column("location_detail")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LocationDetail { get; set; }

    [Column("location_description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? LocationDescription { get; set; }

    [Column("tcp_id")]
    public int? TcpId { get; set; }

    [Column("client_reference_num")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientReferenceNum { get; set; }

    [Column("assigned_inspector")]
    public int? AssignedInspector { get; set; }

    [Column("cp_application_id")]
    public int? CpApplicationId { get; set; }

    [Column("op_application_id")]
    public int? OpApplicationId { get; set; }

    [Column("noi_application_id")]
    public int? NoiApplicationId { get; set; }

    [Column("swo_application_id")]
    public int? SwoApplicationId { get; set; }

    [Column("submission_date", TypeName = "datetime")]
    public DateTime? SubmissionDate { get; set; }

    [Column("submitted_by")]
    public int? SubmittedBy { get; set; }

    [Column("issued_date", TypeName = "datetime")]
    public DateTime? IssuedDate { get; set; }

    [Column("issued_by")]
    public int? IssuedBy { get; set; }

    [Column("reason_for_close")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? ReasonForClose { get; set; }

    [Column("closed_date", TypeName = "datetime")]
    public DateTime? ClosedDate { get; set; }

    [Column("closed_by")]
    public int? ClosedBy { get; set; }

    [Column("cancelled_date", TypeName = "datetime")]
    public DateTime? CancelledDate { get; set; }

    [Column("cancelled_by")]
    public int? CancelledBy { get; set; }

    [Column("rejected_date", TypeName = "datetime")]
    public DateTime? RejectedDate { get; set; }

    [Column("rejected_by")]
    public int? RejectedBy { get; set; }

    [Column("notification_sent_date", TypeName = "datetime")]
    public DateTime? NotificationSentDate { get; set; }

    [Column("last_inspection_date", TypeName = "datetime")]
    public DateTime? LastInspectionDate { get; set; }

    [Column("agency_id")]
    public int? AgencyId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("AgencyId")]
    [InverseProperty("EwrApplications")]
    public virtual Agency? Agency { get; set; }

    [ForeignKey("AppliedBy")]
    [InverseProperty("EwrApplicationAppliedByNavigations")]
    public virtual User? AppliedByNavigation { get; set; }

    [ForeignKey("AssignedInspector")]
    [InverseProperty("EwrApplicationAssignedInspectorNavigations")]
    public virtual User? AssignedInspectorNavigation { get; set; }

    [InverseProperty("EwrApplication")]
    public virtual ICollection<CpApplication> CpApplications { get; set; } = new List<CpApplication>();

    [ForeignKey("EmergencyCategoryId")]
    [InverseProperty("EwrApplications")]
    public virtual EwrEmergencyCategory EmergencyCategory { get; set; } = null!;

    [ForeignKey("EmergencyCauseId")]
    [InverseProperty("EwrApplications")]
    public virtual EwrEmergencyCause? EmergencyCause { get; set; }

    [ForeignKey("EmergencyTypeId")]
    [InverseProperty("EwrApplications")]
    public virtual EwrEmergencyType? EmergencyType { get; set; }

    [InverseProperty("EwrApplication")]
    public virtual ICollection<EwrApplicationLocation> EwrApplicationLocations { get; set; } = new List<EwrApplicationLocation>();

    [ForeignKey("StatusId")]
    [InverseProperty("EwrApplications")]
    public virtual EwrStatus? Status { get; set; }

    [ForeignKey("SwoApplicationId")]
    [InverseProperty("EwrApplications")]
    public virtual SwoApplication? SwoApplication { get; set; }

    [ForeignKey("TcpId")]
    [InverseProperty("EwrApplications")]
    public virtual EwrTcp? Tcp { get; set; }
}
