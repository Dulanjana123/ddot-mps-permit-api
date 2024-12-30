using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_type")]
[Index("SwoTypeId", Name = "IX_swo_type_id")]
[Index("SwoTypeCode", Name = "UK_swo_type_code", IsUnique = true)]
public partial class SwoType
{
    [Key]
    [Column("swo_type_id")]
    public int SwoTypeId { get; set; }

    [Column("swo_type_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string SwoTypeCode { get; set; } = null!;

    [Column("swo_type_desc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SwoTypeDesc { get; set; }

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

    [InverseProperty("SwoType")]
    public virtual ICollection<SwoApplication> SwoApplications { get; set; } = new List<SwoApplication>();
}
