using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_reviewer_teams")]
[Index("PdrmReviewerTeamId", Name = "IX_pdrm_reviewer_team_id")]
public partial class PdrmReviewerTeam
{
    [Key]
    [Column("pdrm_reviewer_team_id")]
    public int PdrmReviewerTeamId { get; set; }

    [Column("meeting_type_id")]
    public int MeetingTypeId { get; set; }

    [Column("team_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string TeamName { get; set; } = null!;

    [Column("team_desc")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? TeamDesc { get; set; }

    [Column("is_default_team")]
    public bool? IsDefaultTeam { get; set; }

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

    [ForeignKey("MeetingTypeId")]
    [InverseProperty("PdrmReviewerTeams")]
    public virtual MeetingType MeetingType { get; set; } = null!;

    [InverseProperty("PdrmReviewerTeam")]
    public virtual ICollection<PdrmReviewerTeamMember> PdrmReviewerTeamMembers { get; set; } = new List<PdrmReviewerTeamMember>();
}
