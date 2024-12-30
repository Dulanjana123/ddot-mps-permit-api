using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_violations")]
[Index("SwoViolationId", Name = "IX_swo_violation_id")]
public partial class SwoViolation
{
    [Key]
    [Column("swo_violation_id")]
    public int SwoViolationId { get; set; }

    [Column("swo_application_id")]
    public int SwoApplicationId { get; set; }

    [Column("swo_violation_type_id")]
    public int? SwoViolationTypeId { get; set; }

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

    [ForeignKey("SwoApplicationId")]
    [InverseProperty("SwoViolations")]
    public virtual SwoApplication SwoApplication { get; set; } = null!;

    [ForeignKey("SwoViolationTypeId")]
    [InverseProperty("SwoViolations")]
    public virtual SwoViolationType? SwoViolationType { get; set; }
}
