using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("submission_material_master")]
[Index("SubmMatMstrId", Name = "IX_subm_mat_mstr_id")]
[Index("SubmMatMstrCode", Name = "UK_subm_mat_mstr_code", IsUnique = true)]
public partial class SubmissionMaterialMaster
{
    [Key]
    [Column("subm_mat_mstr_id")]
    public int SubmMatMstrId { get; set; }

    [Column("subm_mat_mstr_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string SubmMatMstrCode { get; set; } = null!;

    [Column("subm_mat_mstr_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string? SubmMatMstrName { get; set; }

    [Column("subm_mat_mstr_desc")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? SubmMatMstrDesc { get; set; }

    [Column("paper_type_id")]
    public int? PaperTypeId { get; set; }

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

    //[ForeignKey("PaperTypeId")]
    //[InverseProperty("SubmissionMaterialMasters")]
    //public virtual PaperType? PaperType { get; set; }

    [InverseProperty("SubmMatMstr")]
    public virtual ICollection<SubmissionMaterialDetail> SubmissionMaterialDetails { get; set; } = new List<SubmissionMaterialDetail>();
}
