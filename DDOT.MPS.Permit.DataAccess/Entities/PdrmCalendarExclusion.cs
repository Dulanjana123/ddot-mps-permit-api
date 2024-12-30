using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_calendar_exclusions")]
[Index("PdrmCalExclusionId", Name = "IX_pdrm_cal_exclusion_id")]
public partial class PdrmCalendarExclusion
{
    [Key]
    [Column("pdrm_cal_exclusion_id")]
    public int PdrmCalExclusionId { get; set; }

    [Column("pdrm_cal_time_slot_id")]
    public int PdrmCalTimeSlotId { get; set; }

    [Column("excluded_date")]
    public DateOnly ExcludedDate { get; set; }

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

    [ForeignKey("PdrmCalTimeSlotId")]
    [InverseProperty("PdrmCalendarExclusions")]
    public virtual PdrmCalendarTimeSlot PdrmCalTimeSlot { get; set; } = null!;
}
