using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("agencies")]
[Index("AgencyId", Name = "IX_agency_id")]
[Index("AgencyCode", Name = "UK_agency_code", IsUnique = true)]
public partial class Agency
{
    [Key]
    [Column("agency_id")]
    public int AgencyId { get; set; }

    [Column("agency_category_id")]
    public int? AgencyCategoryId { get; set; }

    [Column("agency_code")]
    [StringLength(40)]
    [Unicode(false)]
    public string AgencyCode { get; set; } = null!;

    [Column("agency_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AgencyName { get; set; }

    [Column("agency_address")]
    [StringLength(200)]
    [Unicode(false)]
    public string? AgencyAddress { get; set; }

    [Column("agency_telephone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? AgencyTelephone { get; set; }

    [Column("contact_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ContactName { get; set; }

    [Column("contact_telephone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ContactTelephone { get; set; }

    [Column("contact_email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ContactEmail { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("sort_id")]
    public int? SortId { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("modified_by", TypeName = "datetime")]
    public DateTime? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [ForeignKey("AgencyCategoryId")]
    [InverseProperty("Agencies")]
    public virtual AgencyCategory? AgencyCategory { get; set; }

    [InverseProperty("Agency")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();

    [InverseProperty("Agency")]
    public virtual ICollection<EwrEmergencyTypeAgency> EwrEmergencyTypeAgencies { get; set; } = new List<EwrEmergencyTypeAgency>();

    [InverseProperty("Agency")]
    public virtual ICollection<PdrmApplicationReviewer> PdrmApplicationReviewers { get; set; } = new List<PdrmApplicationReviewer>();

    [InverseProperty("Agency")]
    public virtual ICollection<PdrmNote> PdrmNotes { get; set; } = new List<PdrmNote>();

    [InverseProperty("Agency")]
    public virtual ICollection<PdrmReviewerTeamMember> PdrmReviewerTeamMembers { get; set; } = new List<PdrmReviewerTeamMember>();

    [InverseProperty("Agency")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
