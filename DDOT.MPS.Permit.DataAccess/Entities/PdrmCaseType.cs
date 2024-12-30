using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_case_type")]
[Index("PdrmCaseTypeId", Name = "IX_pdrm_case_type_id")]
[Index("PdrmCaseTypeCode", Name = "UK_pdrm_case_type_code", IsUnique = true)]
public partial class PdrmCaseType
{
    [Key]
    [Column("pdrm_case_type_id")]
    public int PdrmCaseTypeId { get; set; }

    [Column("pdrm_case_type_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string PdrmCaseTypeCode { get; set; } = null!;

    [Column("pdrm_case_type_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PdrmCaseTypeName { get; set; }

    [Column("is_primary")]
    public bool? IsPrimary { get; set; }

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

    [InverseProperty("PdrmCaseType")]
    public virtual ICollection<PdrmApplicationCaseType> PdrmApplicationCaseTypes { get; set; } = new List<PdrmApplicationCaseType>();
}
