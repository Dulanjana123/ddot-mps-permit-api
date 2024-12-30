using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_permits")]
[Index("SwoPermitId", Name = "IX_swo_permit_id")]
public partial class SwoPermit
{
    [Key]
    [Column("swo_permit_id")]
    public int SwoPermitId { get; set; }

    [Column("swo_application_id")]
    public int SwoApplicationId { get; set; }

    [Column("permit_application_id")]
    public int PermitApplicationId { get; set; }

    [Column("permit_application_type_id")]
    public int PermitApplicationTypeId { get; set; }

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

    [ForeignKey("PermitApplicationTypeId")]
    [InverseProperty("SwoPermits")]
    public virtual ApplicationType PermitApplicationType { get; set; } = null!;

    [ForeignKey("SwoApplicationId")]
    [InverseProperty("SwoPermits")]
    public virtual SwoApplication SwoApplication { get; set; } = null!;
}
