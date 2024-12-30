using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("addresses")]
[Index("AddressId", Name = "IX_address_id")]
public partial class Address
{
    [Key]
    [Column("address_id")]
    public int AddressId { get; set; }

    [Column("address_type_id")]
    public int AddressTypeId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("company_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CompanyName { get; set; }

    [Column("unit_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? UnitNumber { get; set; }

    [Column("address_line_1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddressLine1 { get; set; }

    [Column("address_line_2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? AddressLine2 { get; set; }

    [Column("city")]
    [StringLength(80)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(2)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("zip")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Zip { get; set; }

    [Column("business_phone")]
    [StringLength(10)]
    [Unicode(false)]
    public string? BusinessPhone { get; set; }

    [Column("cell_phone")]
    [StringLength(10)]
    [Unicode(false)]
    public string? CellPhone { get; set; }

    [Column("email_address")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EmailAddress { get; set; }

    [Column("tin")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Tin { get; set; }

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

    [ForeignKey("AddressTypeId")]
    [InverseProperty("Addresses")]
    public virtual AddressType AddressType { get; set; } = null!;

    [InverseProperty("AgentAddress")]
    public virtual ICollection<CpApplication> CpApplicationAgentAddresses { get; set; } = new List<CpApplication>();

    [InverseProperty("ContractorAddress")]
    public virtual ICollection<CpApplication> CpApplicationContractorAddresses { get; set; } = new List<CpApplication>();

    [InverseProperty("InspFeePayeeAddress")]
    public virtual ICollection<CpApplication> CpApplicationInspFeePayeeAddresses { get; set; } = new List<CpApplication>();

    [InverseProperty("OwnerAddress")]
    public virtual ICollection<CpApplication> CpApplicationOwnerAddresses { get; set; } = new List<CpApplication>();

    [InverseProperty("PermiteeAddress")]
    public virtual ICollection<CpApplication> CpApplicationPermiteeAddresses { get; set; } = new List<CpApplication>();

    [InverseProperty("WzDepoRefundAddress")]
    public virtual ICollection<CpApplication> CpApplicationWzDepoRefundAddresses { get; set; } = new List<CpApplication>();
}
