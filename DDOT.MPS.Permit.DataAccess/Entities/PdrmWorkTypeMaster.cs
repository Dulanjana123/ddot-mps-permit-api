using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_work_type_master")]
[Index("PdrmWrkTypMstrId", Name = "IX_pdrm_wrk_typ_mstr_id")]
[Index("PdrmWrkTypMstrCode", Name = "UK_pdrm_wrk_typ_mstr_code", IsUnique = true)]
public partial class PdrmWorkTypeMaster
{
    [Key]
    [Column("pdrm_wrk_typ_mstr_id")]
    public int PdrmWrkTypMstrId { get; set; }

    [Column("pdrm_wrk_typ_mstr_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string PdrmWrkTypMstrCode { get; set; } = null!;

    [Column("pdrm_wrk_typ_mstr_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PdrmWrkTypMstrName { get; set; }

    [Column("pdrm_wrk_typ_mstr_desc")]
    [StringLength(200)]
    [Unicode(false)]
    public string? PdrmWrkTypMstrDesc { get; set; }

    [Column("instruction")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? Instruction { get; set; }

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

    [InverseProperty("PdrmWrkTypMstr")]
    public virtual ICollection<PdrmWorkTypeDetail> PdrmWorkTypeDetails { get; set; } = new List<PdrmWorkTypeDetail>();
}
