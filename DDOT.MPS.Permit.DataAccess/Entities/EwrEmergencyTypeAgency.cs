using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_emergency_type_agency")]
[Index("EmergencyTypeAgencyId", Name = "IX_emergency_type_agency_id")]
public partial class EwrEmergencyTypeAgency
{
    [Key]
    [Column("emergency_type_agency_id")]
    public int EmergencyTypeAgencyId { get; set; }

    [Column("emergency_type_id")]
    public int? EmergencyTypeId { get; set; }

    [Column("agency_id")]
    public int? AgencyId { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

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

    [ForeignKey("AgencyId")]
    [InverseProperty("EwrEmergencyTypeAgencies")]
    public virtual Agency? Agency { get; set; }

    [ForeignKey("EmergencyTypeId")]
    [InverseProperty("EwrEmergencyTypeAgencies")]
    public virtual EwrEmergencyType? EmergencyType { get; set; }
}
