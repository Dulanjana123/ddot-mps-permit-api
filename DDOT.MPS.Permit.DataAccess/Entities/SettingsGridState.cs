using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("settings_grid_state")]
[Index("SettingsGridStateId", Name = "IX_settings_grid_state_id")]
public partial class SettingsGridState
{
    [Key]
    [Column("settings_grid_state_id")]
    public int SettingsGridStateId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("interface_id")]
    public int? InterfaceId { get; set; }

    [Column("grid_object_json")]
    [StringLength(18)]
    [Unicode(false)]
    public string? GridObjectJson { get; set; }

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

    [ForeignKey("InterfaceId")]
    [InverseProperty("SettingsGridStates")]
    public virtual Interface? Interface { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SettingsGridStates")]
    public virtual User? User { get; set; }
}
