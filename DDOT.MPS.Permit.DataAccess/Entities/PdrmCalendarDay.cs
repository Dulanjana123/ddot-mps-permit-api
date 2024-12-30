using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_calendar_days")]
[Index("PdrmCalDayId", Name = "IX_pdrm_cal_day_id")]
public partial class PdrmCalendarDay
{
    [Key]
    [Column("pdrm_cal_day_id")]
    public int PdrmCalDayId { get; set; }

    [Column("meeting_type_id")]
    public int MeetingTypeId { get; set; }

    [Column("calendar_day")]
    [StringLength(10)]
    [Unicode(false)]
    public string CalendarDay { get; set; } = null!;

    [Column("is_all_day")]
    public bool? IsAllDay { get; set; }

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
    [InverseProperty("PdrmCalendarDays")]
    public virtual MeetingType MeetingType { get; set; } = null!;
}
