using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("insp_documents")]
[Index("InspDocumentId", Name = "IX_insp_document_id")]
public partial class InspDocument
{
    [Key]
    [Column("insp_document_id")]
    public int InspDocumentId { get; set; }

    [Column("insp_detail_id")]
    public int InspDetailId { get; set; }

    [Column("application_type_id")]
    public int ApplicationTypeId { get; set; }

    [Column("insp_document_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string InspDocumentName { get; set; } = null!;

    [Column("insp_document_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? InspDocumentType { get; set; }

    [Column("insp_document_path")]
    [StringLength(200)]
    [Unicode(false)]
    public string? InspDocumentPath { get; set; }

    [Column("insp_document_file_size_kb")]
    public int? InspDocumentFileSizeKb { get; set; }

    [Column("insp_cloud_document_path")]
    [Unicode(false)]
    public string? InspCloudDocumentPath { get; set; }

    [Column("insp_cloud_uploaded_date", TypeName = "datetime")]
    public DateTime? InspCloudUploadedDate { get; set; }

    [Column("insp_cloud_uploaded_by")]
    public int? InspCloudUploadedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

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

    [ForeignKey("ApplicationTypeId")]
    [InverseProperty("InspDocuments")]
    public virtual ApplicationType ApplicationType { get; set; } = null!;

    [ForeignKey("InspDetailId")]
    [InverseProperty("InspDocuments")]
    public virtual InspDetail InspDetail { get; set; } = null!;
}
