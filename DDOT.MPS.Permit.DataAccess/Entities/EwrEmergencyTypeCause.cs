using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_emergency_type_cause")]
[Index("EmergencyTypeCauseId", Name = "IX_emergency_type_cause_id")]
public partial class EwrEmergencyTypeCause
{
    [Key]
    [Column("emergency_type_cause_id")]
    public int EmergencyTypeCauseId { get; set; }

    [Column("emergency_type_id")]
    public int? EmergencyTypeId { get; set; }

    [Column("emergency_cause_id")]
    public int? EmergencyCauseId { get; set; }

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

    [ForeignKey("EmergencyCauseId")]
    [InverseProperty("EwrEmergencyTypeCauses")]
    public virtual EwrEmergencyCause? EmergencyCause { get; set; }

    [ForeignKey("EmergencyTypeId")]
    [InverseProperty("EwrEmergencyTypeCauses")]
    public virtual EwrEmergencyType? EmergencyType { get; set; }
}
