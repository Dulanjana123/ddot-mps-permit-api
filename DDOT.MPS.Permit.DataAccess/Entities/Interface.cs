using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("interfaces")]
[Index("InterfaceId", Name = "IX_interface_id")]
[Index("Code", Name = "UK_code", IsUnique = true)]
public partial class Interface
{
    [Key]
    [Column("interface_id")]
    public int InterfaceId { get; set; }

    [Column("code")]
    [StringLength(20)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

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

    ////[InverseProperty("Interface")]
    ////public virtual ICollection<ModuleInterfacePermission> ModuleInterfacePermissions { get; set; } = new List<ModuleInterfacePermission>();

    [InverseProperty("Interface")]
    public virtual ICollection<SettingsGridState> SettingsGridStates { get; set; } = new List<SettingsGridState>();
}

