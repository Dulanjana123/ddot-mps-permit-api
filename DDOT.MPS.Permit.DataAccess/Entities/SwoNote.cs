using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_notes")]
[Index("SwoNoteId", Name = "IX_swo_note_id")]
public partial class SwoNote
{
    [Key]
    [Column("swo_note_id")]
    public int SwoNoteId { get; set; }

    [Column("swo_application_id")]
    public int SwoApplicationId { get; set; }

    [Column("note_type_id")]
    public int NoteTypeId { get; set; }

    [Column("notes")]
    [Unicode(false)]
    public string? Notes { get; set; }

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

    [ForeignKey("NoteTypeId")]
    [InverseProperty("SwoNotes")]
    public virtual NoteType NoteType { get; set; } = null!;

    [ForeignKey("SwoApplicationId")]
    [InverseProperty("SwoNotes")]
    public virtual SwoApplication SwoApplication { get; set; } = null!;
}
