using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_scheduling_log")]
[Index("PdrmSchdLogId", Name = "IX_pdrm_schd_log_id")]
public partial class PdrmSchedulingLog
{
    [Key]
    [Column("pdrm_schd_log_id")]
    public int PdrmSchdLogId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("pdrm_status_id")]
    public int? PdrmStatusId { get; set; }

    [Column("scheduled_date", TypeName = "datetime")]
    public DateTime? ScheduledDate { get; set; }

    [Column("scheduled_time_slot_id")]
    public int? ScheduledTimeSlotId { get; set; }

    [Column("comments")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? Comments { get; set; }

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
    [InverseProperty("PdrmSchedulingLogs")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("PdrmStatusId")]
    [InverseProperty("PdrmSchedulingLogs")]
    public virtual PdrmStatus? PdrmStatus { get; set; }

    [ForeignKey("ScheduledTimeSlotId")]
    [InverseProperty("PdrmSchedulingLogs")]
    public virtual PdrmCalendarTimeSlot? ScheduledTimeSlot { get; set; }
}
