using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application")]
[Index("PdrmApplicationId", Name = "IX_pdrm_application_id")]
public partial class PdrmApplication
{
    [Key]
    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("meeting_type_id")]
    public int MeetingTypeId { get; set; }

    [Column("project_id")]
    public int? ProjectId { get; set; }

    [Column("pdrm_status_id")]
    public int? PdrmStatusId { get; set; }

    [Column("applicant_id")]
    public int? ApplicantId { get; set; }

    [Column("proposed_date")]
    public DateOnly? ProposedDate { get; set; }

    [Column("proposed_time_slot_id")]
    public int? ProposedTimeSlotId { get; set; }

    [Column("remarks")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("case_manager_id")]
    public int? CaseManagerId { get; set; }

    [Column("scheduled_date")]
    public DateOnly? ScheduledDate { get; set; }

    [Column("scheduled_time_slot_id")]
    public int? ScheduledTimeSlotId { get; set; }

    [Column("schedule_title")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? ScheduleTitle { get; set; }

    [Column("schedule_body")]
    public string? ScheduleBody { get; set; }

    [Column("case_manager_assigned_by")]
    public int? CaseManagerAssignedBy { get; set; }

    [Column("is_pdrm_flow_complete")]
    public bool? IsPdrmFlowComplete { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("sort_id")]
    public int? SortId { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("ApplicantId")]
    [InverseProperty("PdrmApplicationApplicants")]
    public virtual User? Applicant { get; set; }

    [ForeignKey("CaseManagerId")]
    [InverseProperty("PdrmApplicationCaseManagers")]
    public virtual User? CaseManager { get; set; }

    [ForeignKey("CaseManagerAssignedBy")]
    [InverseProperty("PdrmApplicationCaseManagerAssignedByNavigations")]
    public virtual User? CaseManagerAssignedByNavigation { get; set; }

    [ForeignKey("MeetingTypeId")]
    [InverseProperty("PdrmApplications")]
    public virtual MeetingType MeetingType { get; set; } = null!;

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationAttendee> PdrmApplicationAttendees { get; set; } = new List<PdrmApplicationAttendee>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationCaseType> PdrmApplicationCaseTypes { get; set; } = new List<PdrmApplicationCaseType>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationConversation> PdrmApplicationConversations { get; set; } = new List<PdrmApplicationConversation>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationMinute> PdrmApplicationMinutes { get; set; } = new List<PdrmApplicationMinute>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationReviewer> PdrmApplicationReviewers { get; set; } = new List<PdrmApplicationReviewer>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmApplicationWorkType> PdrmApplicationWorkTypes { get; set; } = new List<PdrmApplicationWorkType>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmDocument> PdrmDocuments { get; set; } = new List<PdrmDocument>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmLocation> PdrmLocations { get; set; } = new List<PdrmLocation>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmNote> PdrmNotes { get; set; } = new List<PdrmNote>();

    [InverseProperty("PdrmApplication")]
    public virtual ICollection<PdrmSchedulingLog> PdrmSchedulingLogs { get; set; } = new List<PdrmSchedulingLog>();

    [ForeignKey("PdrmStatusId")]
    [InverseProperty("PdrmApplications")]
    public virtual PdrmStatus? PdrmStatus { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("PdrmApplications")]
    public virtual Project? Project { get; set; }

    [ForeignKey("ProposedTimeSlotId")]
    [InverseProperty("PdrmApplicationProposedTimeSlots")]
    public virtual PdrmCalendarTimeSlot? ProposedTimeSlot { get; set; }

    [ForeignKey("ScheduledTimeSlotId")]
    [InverseProperty("PdrmApplicationScheduledTimeSlots")]
    public virtual PdrmCalendarTimeSlot? ScheduledTimeSlot { get; set; }
}
