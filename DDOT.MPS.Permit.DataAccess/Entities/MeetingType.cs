using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("meeting_type")]
[Index("MeetingTypeId", Name = "IX_meeting_type_id")]
[Index("MeetingTypeCode", Name = "UK_meeting_type_code", IsUnique = true)]
public partial class MeetingType
{
    [Key]
    [Column("meeting_type_id")]
    public int MeetingTypeId { get; set; }

    [Column("meeting_type_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string MeetingTypeCode { get; set; } = null!;

    [Column("meeting_type_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MeetingTypeName { get; set; }

    [Column("application_fee", TypeName = "decimal(18, 2)")]
    public decimal? ApplicationFee { get; set; }

    [Column("doc_upload_cut_off_days")]
    public int? DocUploadCutOffDays { get; set; }

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

    [InverseProperty("MeetingType")]
    public virtual ICollection<PdrmApplication> PdrmApplications { get; set; } = new List<PdrmApplication>();

    [InverseProperty("MeetingType")]
    public virtual ICollection<PdrmCalendarDay> PdrmCalendarDays { get; set; } = new List<PdrmCalendarDay>();

    [InverseProperty("MeetingType")]
    public virtual ICollection<PdrmCalendarTimeSlot> PdrmCalendarTimeSlots { get; set; } = new List<PdrmCalendarTimeSlot>();

    [InverseProperty("MeetingType")]
    public virtual ICollection<PdrmReviewerTeam> PdrmReviewerTeams { get; set; } = new List<PdrmReviewerTeam>();
}
