using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application_case_types")]
[Index("PdrmAppCaseTypeId", Name = "IX_pdrm_app_case_type_id")]
public partial class PdrmApplicationCaseType
{
    [Key]
    [Column("pdrm_app_case_type_id")]
    public int PdrmAppCaseTypeId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("pdrm_case_type_id")]
    public int PdrmCaseTypeId { get; set; }

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

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmApplicationCaseTypes")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("PdrmCaseTypeId")]
    [InverseProperty("PdrmApplicationCaseTypes")]
    public virtual PdrmCaseType PdrmCaseType { get; set; } = null!;
}
