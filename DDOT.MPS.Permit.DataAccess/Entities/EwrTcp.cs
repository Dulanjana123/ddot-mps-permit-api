using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_tcp")]
[Index("TcpId", Name = "IX_tcp_id")]
public partial class EwrTcp
{
    [Key]
    [Column("tcp_id")]
    public int TcpId { get; set; }

    [Column("tcp_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TcpName { get; set; }

    [Column("tcp_image_path")]
    [StringLength(200)]
    [Unicode(false)]
    public string? TcpImagePath { get; set; }

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

    [InverseProperty("Tcp")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();
}
