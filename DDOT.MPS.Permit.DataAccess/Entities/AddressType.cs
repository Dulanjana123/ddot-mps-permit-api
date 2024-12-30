using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("address_type")]
[Index("AddressTypeId", Name = "IX_address_type_id")]
[Index("AddressTypeCode", Name = "UK_address_type_code", IsUnique = true)]
public partial class AddressType
{
    [Key]
    [Column("address_type_id")]
    public int AddressTypeId { get; set; }

    [Column("address_type_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string AddressTypeCode { get; set; } = null!;

    [Column("address_type_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? AddressTypeName { get; set; }

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

    [InverseProperty("AddressType")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
