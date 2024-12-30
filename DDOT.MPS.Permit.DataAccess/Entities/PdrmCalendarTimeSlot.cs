using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_calendar_time_slots")]
[Index("PdrmCalTimeSlotId", Name = "IX_pdrm_cal_time_slot_id")]
public partial class PdrmCalendarTimeSlot
{
    [Key]
    [Column("pdrm_cal_time_slot_id")]
    public int PdrmCalTimeSlotId { get; set; }

    [Column("meeting_type_id")]
    public int MeetingTypeId { get; set; }

    [Column("from_time")]
    public TimeOnly FromTime { get; set; }

    [Column("to_time")]
    public TimeOnly ToTime { get; set; }

    [Column("time_duration_minutes")]
    public int? TimeDurationMinutes { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

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

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("MeetingTypeId")]
    [InverseProperty("PdrmCalendarTimeSlots")]
    public virtual MeetingType MeetingType { get; set; } = null!;

    [InverseProperty("ProposedTimeSlot")]
    public virtual ICollection<PdrmApplication> PdrmApplicationProposedTimeSlots { get; set; } = new List<PdrmApplication>();

    [InverseProperty("ScheduledTimeSlot")]
    public virtual ICollection<PdrmApplication> PdrmApplicationScheduledTimeSlots { get; set; } = new List<PdrmApplication>();

    [InverseProperty("PdrmCalTimeSlot")]
    public virtual ICollection<PdrmCalendarExclusion> PdrmCalendarExclusions { get; set; } = new List<PdrmCalendarExclusion>();

    [InverseProperty("ScheduledTimeSlot")]
    public virtual ICollection<PdrmSchedulingLog> PdrmSchedulingLogs { get; set; } = new List<PdrmSchedulingLog>();
}
