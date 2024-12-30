using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("cp_status")]
[Index("CpStatusId", Name = "IX_cp_status_id")]
[Index("CpStatusCode", Name = "UK_cp_status_code", IsUnique = true)]
public partial class CpStatus
{
    [Key]
    [Column("cp_status_id")]
    public int CpStatusId { get; set; }

    [Column("cp_status_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string CpStatusCode { get; set; } = null!;

    [Column("cp_status_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CpStatusName { get; set; }

    [Column("cp_status_desc")]
    [StringLength(200)]
    [Unicode(false)]
    public string? CpStatusDesc { get; set; }

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

    [InverseProperty("CpStatus")]
    public virtual ICollection<CpApplication> CpApplications { get; set; } = new List<CpApplication>();
}
