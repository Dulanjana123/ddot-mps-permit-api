using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DDOT.MPS.Permit.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("insp_details")]
[Index("InspDetailId", Name = "IX_insp_detail_id")]
public partial class InspDetail
{
    [Key]
    [Column("insp_detail_id")]
    public int InspDetailId { get; set; }

    [Column("application_id")]
    public int ApplicationId { get; set; }

    [Column("application_type_id")]
    public int ApplicationTypeId { get; set; }

    [Column("application_status_id")]
    public int? ApplicationStatusId { get; set; }

    [Column("inspected_by")]
    public int InspectedBy { get; set; }

    [Column("insp_type_id")]
    public int? InspTypeId { get; set; }

    [Column("insp_status_id")]
    public int? InspStatusId { get; set; }

    [Column("inspection_date", TypeName = "datetime")]
    public DateTime? InspectionDate { get; set; }

    [Column("minutes_spent")]
    public int? MinutesSpent { get; set; }

    [Column("comments")]
    [Unicode(false)]
    public string? Comments { get; set; }

    [Column("internal_notes")]
    [Unicode(false)]
    public string? InternalNotes { get; set; }

    [Column("external_notes")]
    [Unicode(false)]
    public string? ExternalNotes { get; set; }

    [Column("notes_for_applicant")]
    [Unicode(false)]
    public string? NotesForApplicant { get; set; }

    [Column("internal_notes_modified_by")]
    public int? InternalNotesModifiedBy { get; set; }

    [Column("internal_notes_modified_date", TypeName = "datetime")]
    public DateTime? InternalNotesModifiedDate { get; set; }

    [Column("external_notes_modified_by")]
    public int? ExternalNotesModifiedBy { get; set; }

    [Column("external_notes_modified_date", TypeName = "datetime")]
    public DateTime? ExternalNotesModifiedDate { get; set; }

    [Column("notes_for_applicant_modified_by")]
    public int? NotesForApplicantModifiedBy { get; set; }

    [Column("notes_for_applicant_modified_date", TypeName = "datetime")]
    public DateTime? NotesForApplicantModifiedDate { get; set; }

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

    [ForeignKey("ApplicationTypeId")]
    [InverseProperty("InspDetails")]
    public virtual ApplicationType ApplicationType { get; set; } = null!;

    [InverseProperty("InspDetail")]
    public virtual ICollection<InspDocument> InspDocuments { get; set; } = new List<InspDocument>();

    [ForeignKey("InspStatusId")]
    [InverseProperty("InspDetails")]
    public virtual InspStatus? InspStatus { get; set; }

    [ForeignKey("InspectedBy")]
    [InverseProperty("InspDetails")]
    public virtual User InspectedByNavigation { get; set; } = null!;
}
