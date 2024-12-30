using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project_role")]
[Index("ProjectRoleId", Name = "IX_project_role_id")]
[Index("ProjectRoleCode", Name = "UK_project_role_code", IsUnique = true)]
public partial class ProjectRole
{
    [Key]
    [Column("project_role_id")]
    public int ProjectRoleId { get; set; }

    [Column("project_role_code")]
    [StringLength(25)]
    [Unicode(false)]
    public string ProjectRoleCode { get; set; } = null!;

    [Column("project_role_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ProjectRoleName { get; set; }

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

    [InverseProperty("ProjectRole")]
    public virtual ICollection<PdrmApplicationAttendee> PdrmApplicationAttendees { get; set; } = new List<PdrmApplicationAttendee>();

    [InverseProperty("ProjectRole")]
    public virtual ICollection<ProjectApplicantTeam> ProjectApplicantTeams { get; set; } = new List<ProjectApplicantTeam>();
}
