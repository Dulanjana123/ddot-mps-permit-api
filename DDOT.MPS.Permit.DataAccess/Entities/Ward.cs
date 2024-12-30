using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ward")]
[Index("WardId", Name = "IX_ward_id")]
[Index("WardName", Name = "UK_ward_name", IsUnique = true)]
public partial class Ward
{
    [Key]
    [Column("ward_id")]
    public int WardId { get; set; }

    [Column("ward_name")]
    [StringLength(20)]
    [Unicode(false)]
    public string WardName { get; set; } = null!;

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

    [InverseProperty("Ward")]
    public virtual ICollection<Anc> Ancs { get; set; } = new List<Anc>();

    [InverseProperty("Ward")]
    public virtual ICollection<ProjectLocationWardAnc> ProjectLocationWardAncs { get; set; } = new List<ProjectLocationWardAnc>();
}
