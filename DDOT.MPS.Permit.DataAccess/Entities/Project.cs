using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project")]
[Index("ProjectId", Name = "IX_project_id")]
public partial class Project
{
    [Key]
    [Column("project_id")]
    public int ProjectId { get; set; }

    [Column("project_name")]
    [StringLength(500)]
    [Unicode(false)]
    public string ProjectName { get; set; } = null!;

    [Column("project_applicant_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ProjectApplicantName { get; set; }

    [Column("project_description")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? ProjectDescription { get; set; }

    [Column("project_start_date", TypeName = "datetime")]
    public DateTime? ProjectStartDate { get; set; }

    [Column("project_end_date", TypeName = "datetime")]
    public DateTime? ProjectEndDate { get; set; }

    [Column("project_contact_user_id")]
    public int? ProjectContactUserId { get; set; }

    [Column("project_location_json")]
    public string? ProjectLocationJson { get; set; }

    [Column("bza_zc_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BzaZcNumber { get; set; }

    [Column("so_sp_ltr_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SoSpLtrNumber { get; set; }

    [Column("dob_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DobNumber { get; set; }

    [Column("eisf_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EisfNumber { get; set; }

    [Column("eisf_submission_date", TypeName = "datetime")]
    public DateTime? EisfSubmissionDate { get; set; }

    [Column("is_eisf_approved")]
    public bool? IsEisfApproved { get; set; }

    [Column("project_status_id")]
    public int? ProjectStatusId { get; set; }

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

    [InverseProperty("Project")]
    public virtual ICollection<CpApplication> CpApplications { get; set; } = new List<CpApplication>();

    [InverseProperty("Project")]
    public virtual ICollection<PdrmApplication> PdrmApplications { get; set; } = new List<PdrmApplication>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectApplicantTeam> ProjectApplicantTeams { get; set; } = new List<ProjectApplicantTeam>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectCoreTeam> ProjectCoreTeams { get; set; } = new List<ProjectCoreTeam>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectLocation> ProjectLocations { get; set; } = new List<ProjectLocation>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectSupportTeam> ProjectSupportTeams { get; set; } = new List<ProjectSupportTeam>();
    
    //Manually added
    [ForeignKey("ProjectContactUserId")]
    [InverseProperty("Projects")]
    public virtual User ProjectContactUser { get; set; }
}
