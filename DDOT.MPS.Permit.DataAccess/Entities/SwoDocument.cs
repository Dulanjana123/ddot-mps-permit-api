using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_documents")]
[Index("SwoDocumentId", Name = "IX_swo_document_id")]
public partial class SwoDocument
{
    [Key]
    [Column("swo_document_id")]
    public int SwoDocumentId { get; set; }

    [Column("swo_application_id")]
    public int SwoApplicationId { get; set; }

    [Column("document_name")]
    [StringLength(200)]
    [Unicode(false)]
    public string? DocumentName { get; set; }

    // didn't found in tempentity
    //[Column("physical_file_name")]
    //[StringLength(200)]
    //[Unicode(false)]
    //public string? PhysicalFileName { get; set; }
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

    // has chanded from cloud_document_path to _cloud_document_path
    [Column("_cloud_document_path")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? CloudDocumentPath { get; set; }

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

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("SwoApplicationId")]
    [InverseProperty("SwoDocuments")]
    public virtual SwoApplication SwoApplication { get; set; } = null!;
}
