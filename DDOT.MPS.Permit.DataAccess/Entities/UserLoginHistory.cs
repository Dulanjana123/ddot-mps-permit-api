using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("user_login_history")]
[Index("Id", Name = "IX_login_history_id")]
public partial class UserLoginHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_intraction_id")]
    public int? UserIntractionId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("time_stamp", TypeName = "datetime")]
    public DateTime? TimeStamp { get; set; }

    [Column("detailed_description", TypeName = "text")]
    public string? DetailedDescription { get; set; }

    [Column("browser")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Browser { get; set; }

    [Column("os")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Os { get; set; }

    [Column("device")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Device { get; set; }

    [Column("ip_address")]
    [StringLength(45)]
    [Unicode(false)]
    public string? IpAddress { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserLoginHistories")]
    public virtual User? User { get; set; }
}
