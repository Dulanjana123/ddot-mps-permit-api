using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_status")]
[Index("SwoStatusId", Name = "IX_swo_status_id")]
[Index("SwoStatusCode", Name = "UK_swo_status_code", IsUnique = true)]
public partial class SwoStatus
{
    [Key]
    [Column("swo_status_id")]
    public int SwoStatusId { get; set; }

    [Column("swo_status_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string SwoStatusCode { get; set; } = null!;

    [Column("swo_status_desc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SwoStatusDesc { get; set; }

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

    [InverseProperty("SwoStatus")]
    public virtual ICollection<SwoApplication> SwoApplications { get; set; } = new List<SwoApplication>();
}
