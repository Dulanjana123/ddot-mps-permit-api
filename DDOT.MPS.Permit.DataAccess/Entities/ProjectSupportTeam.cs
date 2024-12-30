using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project_support_team")]
[Index("ProjectSupportTeamId", Name = "IX_project_support_team_id")]
public partial class ProjectSupportTeam
{
    [Key]
    [Column("project_support_team_id")]
    public int ProjectSupportTeamId { get; set; }

    [Column("project_id")]
    public int ProjectId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

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

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectSupportTeams")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ProjectSupportTeams")]
    public virtual User User { get; set; } = null!;
}
