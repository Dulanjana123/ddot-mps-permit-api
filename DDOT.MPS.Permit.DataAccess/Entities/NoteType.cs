using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("note_type")]
[Index("NoteTypeId", Name = "IX_note_type_id")]
[Index("NoteCode", Name = "UK_note_code", IsUnique = true)]
public partial class NoteType
{
    [Key]
    [Column("note_type_id")]
    public int NoteTypeId { get; set; }

    [Column("note_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string NoteCode { get; set; } = null!;

    [Column("note_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? NoteName { get; set; }

    [Column("note_description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NoteDescription { get; set; }

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

    [InverseProperty("NoteType")]
    public virtual ICollection<PdrmNote> PdrmNotes { get; set; } = new List<PdrmNote>();

    [InverseProperty("NoteType")]
    public virtual ICollection<SwoNote> SwoNotes { get; set; } = new List<SwoNote>();
}
