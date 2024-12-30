using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_locations")]
[Index("PdrmLocationId", Name = "IX_pdrm_location_id")]
public partial class PdrmLocation
{
    [Key]
    [Column("pdrm_location_id")]
    public int PdrmLocationId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("project_location_id")]
    public int ProjectLocationId { get; set; }

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

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmLocations")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("ProjectLocationId")]
    [InverseProperty("PdrmLocations")]
    public virtual ProjectLocation ProjectLocation { get; set; } = null!;
}
