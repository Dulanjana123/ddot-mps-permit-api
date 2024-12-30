using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application_attendees")]
[Index("PdrmAppAttendeeId", Name = "IX_pdrm_app_attendee_id")]
public partial class PdrmApplicationAttendee
{
    [Key]
    [Column("pdrm_app_attendee_id")]
    public int PdrmAppAttendeeId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("attendee_id")]
    public int AttendeeId { get; set; }

    [Column("project_role_id")]
    public int ProjectRoleId { get; set; }

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

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("AttendeeId")]
    [InverseProperty("PdrmApplicationAttendees")]
    public virtual User Attendee { get; set; } = null!;

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmApplicationAttendees")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;

    [ForeignKey("ProjectRoleId")]
    [InverseProperty("PdrmApplicationAttendees")]
    public virtual ProjectRole ProjectRole { get; set; } = null!;
}
