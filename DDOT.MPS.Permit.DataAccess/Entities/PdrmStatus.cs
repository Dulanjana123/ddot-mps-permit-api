using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_status")]
[Index("PdrmStatusId", Name = "IX_pdrm_status_id")]
[Index("PdrmStatusCode", Name = "UK_pdrm_status_code", IsUnique = true)]
public partial class PdrmStatus
{
    [Key]
    [Column("pdrm_status_id")]
    public int PdrmStatusId { get; set; }

    [Column("pdrm_status_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string PdrmStatusCode { get; set; } = null!;

    [Column("pdrm_status_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PdrmStatusName { get; set; }

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

    [InverseProperty("PdrmStatus")]
    public virtual ICollection<PdrmApplication> PdrmApplications { get; set; } = new List<PdrmApplication>();

    [InverseProperty("PdrmStatus")]
    public virtual ICollection<PdrmSchedulingLog> PdrmSchedulingLogs { get; set; } = new List<PdrmSchedulingLog>();
}
