using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("user_type")]
[Index("UserTypeId", Name = "IX_user_type_id")]
[Index("UserTypeCode", Name = "UK_user_type_code", IsUnique = true)]
public partial class UserType
{
    [Key]
    [Column("user_type_id")]
    public int UserTypeId { get; set; }

    [Column("user_type_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string UserTypeCode { get; set; } = null!;

    [Column("user_type_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? UserTypeName { get; set; }

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

    [InverseProperty("UserType")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
