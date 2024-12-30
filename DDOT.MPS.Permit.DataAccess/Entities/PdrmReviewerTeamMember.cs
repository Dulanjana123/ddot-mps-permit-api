using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_reviewer_team_members")]
[Index("PdrmReviewerTeamMemberId", Name = "IX_pdrm_reviewer_team_member_id")]
public partial class PdrmReviewerTeamMember
{
    [Key]
    [Column("pdrm_reviewer_team_member_id")]
    public int PdrmReviewerTeamMemberId { get; set; }

    [Column("pdrm_reviewer_team_id")]
    public int PdrmReviewerTeamId { get; set; }

    [Column("agency_id")]
    public int? AgencyId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

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

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("AgencyId")]
    [InverseProperty("PdrmReviewerTeamMembers")]
    public virtual Agency? Agency { get; set; }

    [ForeignKey("PdrmReviewerTeamId")]
    [InverseProperty("PdrmReviewerTeamMembers")]
    public virtual PdrmReviewerTeam PdrmReviewerTeam { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("PdrmReviewerTeamMembers")]
    public virtual User? User { get; set; }
}
