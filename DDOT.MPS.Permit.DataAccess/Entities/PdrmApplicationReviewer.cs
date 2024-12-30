using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application_reviewers")]
[Index("PdrmAppReviewerId", Name = "IX_pdrm_app_reviewer_id")]
public partial class PdrmApplicationReviewer
{
    [Key]
    [Column("pdrm_app_reviewer_id")]
    public int PdrmAppReviewerId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("agency_id")]
    public int? AgencyId { get; set; }

    [Column("reviewer_id")]
    public int ReviewerId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("is_reviewer_available")]
    public bool? IsReviewerAvailable { get; set; }

    [Column("is_reviewer_active")]
    public bool? IsReviewerActive { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("attendance_status_legacy")]
    [StringLength(20)]
    [Unicode(false)]
    public string? AttendanceStatusLegacy { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("AgencyId")]
    [InverseProperty("PdrmApplicationReviewers")]
    public virtual Agency? Agency { get; set; }

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmApplicationReviewers")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("ReviewerId")]
    [InverseProperty("PdrmApplicationReviewers")]
    public virtual User Reviewer { get; set; } = null!;
}
