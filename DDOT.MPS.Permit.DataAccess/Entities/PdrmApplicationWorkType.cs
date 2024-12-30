using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application_work_types")]
[Index("PdrmAppWorkTypeId", Name = "IX_pdrm_app_work_type_id")]
public partial class PdrmApplicationWorkType
{
    [Key]
    [Column("pdrm_app_work_type_id")]
    public int PdrmAppWorkTypeId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("pdrm_wrk_typ_dtl_id")]
    public int PdrmWrkTypDtlId { get; set; }

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

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmApplicationWorkTypes")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("PdrmWrkTypDtlId")]
    [InverseProperty("PdrmApplicationWorkTypes")]
    public virtual PdrmWorkTypeDetail PdrmWrkTypDtl { get; set; } = null!;
}
