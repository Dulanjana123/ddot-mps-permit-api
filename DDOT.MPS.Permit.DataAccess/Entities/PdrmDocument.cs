using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_documents")]
[Index("PdrmDocumentId", Name = "IX_pdrm_document_id")]
public partial class PdrmDocument
{
    [Key]
    [Column("pdrm_document_id")]
    public int PdrmDocumentId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("subm_mat_dtl_id")]
    public int SubmMatDtlId { get; set; }

    [Column("document_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string? DocumentName { get; set; }

    [Column("document_path")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? DocumentPath { get; set; }

    [Column("document_file_size_kb")]
    public int? DocumentFileSizeKb { get; set; }

    [Column("file_type")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FileType { get; set; }

    [Column("is_online_submission")]
    public bool? IsOnlineSubmission { get; set; }

    [Column("cloud_document_path")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? CloudDocumentPath { get; set; }

    [Column("remarks")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("is_mandatory")]
    public bool? IsMandatory { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

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
    [InverseProperty("PdrmDocuments")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("SubmMatDtlId")]
    [InverseProperty("PdrmDocuments")]
    public virtual SubmissionMaterialDetail SubmMatDtl { get; set; } = null!;
}
