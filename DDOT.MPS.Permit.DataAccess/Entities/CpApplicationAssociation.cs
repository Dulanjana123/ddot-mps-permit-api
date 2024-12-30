using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("cp_application_associations")]
[Index("CpAppAssociationId", Name = "IX_cp_app_association_id")]
public partial class CpApplicationAssociation
{
    [Key]
    [Column("cp_app_association_id")]
    public int CpAppAssociationId { get; set; }

    [Column("cp_application_id")]
    public int CpApplicationId { get; set; }

    [Column("associated_application_id")]
    public int AssociatedApplicationId { get; set; }

    [Column("associated_application_type_id")]
    public int AssociatedApplicationTypeId { get; set; }

    [Column("cp_association_type_id")]
    public int? CpAssociationTypeId { get; set; }

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

    [ForeignKey("AssociatedApplicationTypeId")]
    [InverseProperty("CpApplicationAssociations")]
    public virtual ApplicationType AssociatedApplicationType { get; set; } = null!;

    [ForeignKey("CpApplicationId")]
    [InverseProperty("CpApplicationAssociations")]
    public virtual CpApplication CpApplication { get; set; } = null!;

    [ForeignKey("CpAssociationTypeId")]
    [InverseProperty("CpApplicationAssociations")]
    public virtual CpAssociationType? CpAssociationType { get; set; }
}
