using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_work_type_detail")]
[Index("PdrmWrkTypDtlId", Name = "IX_pdrm_wrk_typ_dtl_id")]
public partial class PdrmWorkTypeDetail
{
    [Key]
    [Column("pdrm_wrk_typ_dtl_id")]
    public int PdrmWrkTypDtlId { get; set; }

    [Column("pdrm_wrk_typ_mstr_id")]
    public int PdrmWrkTypMstrId { get; set; }

    [Column("pdrm_wrk_typ_dtl_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PdrmWrkTypDtlName { get; set; }

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

    [InverseProperty("PdrmWrkTypDtl")]
    public virtual ICollection<PdrmApplicationWorkType> PdrmApplicationWorkTypes { get; set; } = new List<PdrmApplicationWorkType>();

    [ForeignKey("PdrmWrkTypMstrId")]
    [InverseProperty("PdrmWorkTypeDetails")]
    public virtual PdrmWorkTypeMaster PdrmWrkTypMstr { get; set; } = null!;
}
