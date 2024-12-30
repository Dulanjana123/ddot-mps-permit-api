using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("anc")]
[Index("AncId", Name = "IX_anc_id")]
public partial class Anc
{
    [Key]
    [Column("anc_id")]
    public int AncId { get; set; }

    [Column("ward_id")]
    public int WardId { get; set; }

    [Column("anc_name")]
    [StringLength(20)]
    [Unicode(false)]
    public string AncName { get; set; } = null!;

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

    [InverseProperty("Anc")]
    public virtual ICollection<ProjectLocationWardAnc> ProjectLocationWardAncs { get; set; } = new List<ProjectLocationWardAnc>();

    [ForeignKey("WardId")]
    [InverseProperty("Ancs")]
    public virtual Ward Ward { get; set; } = null!;
}
