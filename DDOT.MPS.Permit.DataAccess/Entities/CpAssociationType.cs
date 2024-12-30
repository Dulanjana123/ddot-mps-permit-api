using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("cp_association_type")]
[Index("CpAssociationTypeId", Name = "IX_cp_association_type_id")]
[Index("CpAssociationTypeCode", Name = "UK_cp_association_type_code", IsUnique = true)]
public partial class CpAssociationType
{
    [Key]
    [Column("cp_association_type_id")]
    public int CpAssociationTypeId { get; set; }

    [Column("cp_association_type_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string CpAssociationTypeCode { get; set; } = null!;

    [Column("cp_association_type_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CpAssociationTypeName { get; set; }

    [Column("cp_association_type_desc")]
    [StringLength(2000)]
    [Unicode(false)]
    public string? CpAssociationTypeDesc { get; set; }

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

    [InverseProperty("CpAssociationType")]
    public virtual ICollection<CpApplicationAssociation> CpApplicationAssociations { get; set; } = new List<CpApplicationAssociation>();
}
