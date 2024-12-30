using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project_location_ward_anc")]
[Index("ProjectLocWardAncId", Name = "IX_project_loc_ward_anc_id")]
public partial class ProjectLocationWardAnc
{
    [Key]
    [Column("project_loc_ward_anc_id")]
    public int ProjectLocWardAncId { get; set; }

    [Column("project_location_id")]
    public int ProjectLocationId { get; set; }

    [Column("ward_id")]
    public int? WardId { get; set; }

    [Column("anc_id")]
    public int? AncId { get; set; }

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

    [ForeignKey("AncId")]
    [InverseProperty("ProjectLocationWardAncs")]
    public virtual Anc? Anc { get; set; }

    [ForeignKey("ProjectLocationId")]
    [InverseProperty("ProjectLocationWardAncs")]
    public virtual ProjectLocation ProjectLocation { get; set; } = null!;

    [ForeignKey("WardId")]
    [InverseProperty("ProjectLocationWardAncs")]
    public virtual Ward? Ward { get; set; }
}
