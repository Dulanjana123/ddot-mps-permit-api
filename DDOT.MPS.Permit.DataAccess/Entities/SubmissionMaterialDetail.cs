using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("submission_material_detail")]
[Index("SubmMatDtlId", Name = "IX_subm_mat_dtl_id")]
[Index("SubmMatDtlCode", Name = "UK_subm_mat_dtl_code", IsUnique = true)]
public partial class SubmissionMaterialDetail
{
    [Key]
    [Column("subm_mat_dtl_id")]
    public int SubmMatDtlId { get; set; }

    [Column("subm_mat_mstr_id")]
    public int SubmMatMstrId { get; set; }

    [Column("subm_mat_dtl_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string SubmMatDtlCode { get; set; } = null!;

    [Column("subm_mat_dtl_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string? SubmMatDtlName { get; set; }

    [Column("subm_mat_dtl_desc")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? SubmMatDtlDesc { get; set; }

    [Column("number_of_copies")]
    public int? NumberOfCopies { get; set; }

    [Column("instructions")]
    [Unicode(false)]
    public string? Instructions { get; set; }

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

    [InverseProperty("SubmMatDtl")]
    public virtual ICollection<PdrmApplicationMinute> PdrmApplicationMinutes { get; set; } = new List<PdrmApplicationMinute>();

    [InverseProperty("SubmMatDtl")]
    public virtual ICollection<PdrmDocument> PdrmDocuments { get; set; } = new List<PdrmDocument>();

    [ForeignKey("SubmMatMstrId")]
    [InverseProperty("SubmissionMaterialDetails")]
    public virtual SubmissionMaterialMaster SubmMatMstr { get; set; } = null!;
}
