using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_notes")]
[Index("PdrmNoteId", Name = "IX_pdrm_note_id")]
public partial class PdrmNote
{
    [Key]
    [Column("pdrm_note_id")]
    public int PdrmNoteId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("note_type_id")]
    public int NoteTypeId { get; set; }

    [Column("agency_id")]
    public int? AgencyId { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

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

    [ForeignKey("AgencyId")]
    [InverseProperty("PdrmNotes")]
    public virtual Agency? Agency { get; set; }

    [ForeignKey("NoteTypeId")]
    [InverseProperty("PdrmNotes")]
    public virtual NoteType NoteType { get; set; } = null!;

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmNotes")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;
}
