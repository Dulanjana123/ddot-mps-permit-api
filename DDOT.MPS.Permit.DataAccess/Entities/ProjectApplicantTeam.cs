using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project_applicant_team")]
[Index("ProjectApplicantTeamId", Name = "IX_project_applicant_team_id")]
public partial class ProjectApplicantTeam
{
    [Key]
    [Column("project_applicant_team_id")]
    public int ProjectApplicantTeamId { get; set; }

    [Column("project_id")]
    public int ProjectId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("project_role_id")]
    public int ProjectRoleId { get; set; }

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

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectApplicantTeams")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("ProjectRoleId")]
    [InverseProperty("ProjectApplicantTeams")]
    public virtual ProjectRole ProjectRole { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ProjectApplicantTeams")]
    public virtual User User { get; set; } = null!;
}
