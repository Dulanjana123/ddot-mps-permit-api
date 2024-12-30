using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_violation_type")]
[Index("SwoViolationTypeId", Name = "IX_swo_violation_type_id")]
[Index("SwoViolTypeCode", Name = "UK_swo_violation_type_code", IsUnique = true)]
public partial class SwoViolationType
{
    [Key]
    [Column("swo_violation_type_id")]
    public int SwoViolationTypeId { get; set; }

    [Column("swo_viol_type_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string SwoViolTypeCode { get; set; } = null!;

    [Column("swo_viol_type_desc")]
    [StringLength(200)]
    [Unicode(false)]
    public string? SwoViolTypeDesc { get; set; }

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

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [InverseProperty("SwoViolationType")]
    public virtual ICollection<SwoViolation> SwoViolations { get; set; } = new List<SwoViolation>();
}
