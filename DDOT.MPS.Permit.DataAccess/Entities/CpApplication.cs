using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("cp_application")]
[Index("CpApplicationId", Name = "IX_cp_application_id")]
[Index("CpTrackingNumber", Name = "UK_cp_tracking_number", IsUnique = true)]
public partial class CpApplication
{
    [Key]
    [Column("cp_application_id")]
    public int CpApplicationId { get; set; }

    [Column("cp_tracking_number")]
    public int CpTrackingNumber { get; set; }

    [Column("project_id")]
    public int? ProjectId { get; set; }

    [Column("ewr_application_id")]
    public int? EwrApplicationId { get; set; }

    [Column("is_project_associated")]
    public bool? IsProjectAssociated { get; set; }

    [Column("is_ewr_associated")]
    public bool? IsEwrAssociated { get; set; }

    [Column("is_existing_cp_associated")]
    public bool? IsExistingCpAssociated { get; set; }

    [Column("is_draft_application")]
    public bool? IsDraftApplication { get; set; }

    [Column("submited_date", TypeName = "datetime")]
    public DateTime? SubmitedDate { get; set; }

    [Column("submitted_by")]
    public int? SubmittedBy { get; set; }

    [Column("cp_status_id")]
    public int? CpStatusId { get; set; }

    [Column("is_public_space_needed")]
    public bool? IsPublicSpaceNeeded { get; set; }

    [Column("work_in_detail")]
    public string? WorkInDetail { get; set; }

    [Column("business_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BusinessName { get; set; }

    [Column("business_owner_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BusinessOwnerName { get; set; }

    [Column("proposed_start_date", TypeName = "datetime")]
    public DateTime ProposedStartDate { get; set; }

    [Column("proposed_end_date", TypeName = "datetime")]
    public DateTime ProposedEndDate { get; set; }

    [Column("bza_zc_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BzaZcNumber { get; set; }

    [Column("so_sp_ltr_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SoSpLtrNumber { get; set; }

    [Column("dob_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DobNumber { get; set; }

    [Column("eisf_number")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EisfNumber { get; set; }

    [Column("eisf_submitted_date", TypeName = "datetime")]
    public DateTime? EisfSubmittedDate { get; set; }

    [Column("is_eisf_approved")]
    public bool? IsEisfApproved { get; set; }

    [Column("agent_address_id")]
    public int? AgentAddressId { get; set; }

    [Column("contractor_address_id")]
    public int? ContractorAddressId { get; set; }

    [Column("wz_depo_refund_address_id")]
    public int? WzDepoRefundAddressId { get; set; }

    [Column("insp_fee_payee_address_id")]
    public int? InspFeePayeeAddressId { get; set; }

    [Column("permitee_address_id")]
    public int? PermiteeAddressId { get; set; }

    [Column("owner_address_id")]
    public int? OwnerAddressId { get; set; }

    [Column("intake_technician_id")]
    public int? IntakeTechnicianId { get; set; }

    [Column("technician_intake_date", TypeName = "datetime")]
    public DateTime? TechnicianIntakeDate { get; set; }

    [Column("assigned_technician_id")]
    public int? AssignedTechnicianId { get; set; }

    [Column("technician_assigned_date", TypeName = "datetime")]
    public DateTime? TechnicianAssignedDate { get; set; }

    [Column("assigned_inspector_id")]
    public int? AssignedInspectorId { get; set; }

    [Column("inspector_assigned_date", TypeName = "datetime")]
    public DateTime? InspectorAssignedDate { get; set; }

    [Column("is_psc_review_required")]
    public bool? IsPscReviewRequired { get; set; }

    [Column("psc_review_status_id")]
    public int? PscReviewStatusId { get; set; }

    [Column("psc_hearing_date", TypeName = "datetime")]
    public DateTime? PscHearingDate { get; set; }

    [Column("last_review_due_date", TypeName = "datetime")]
    public DateTime? LastReviewDueDate { get; set; }

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

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("AgentAddressId")]
    [InverseProperty("CpApplicationAgentAddresses")]
    public virtual Address? AgentAddress { get; set; }

    [ForeignKey("AssignedInspectorId")]
    [InverseProperty("CpApplicationAssignedInspectors")]
    public virtual User? AssignedInspector { get; set; }

    [ForeignKey("AssignedTechnicianId")]
    [InverseProperty("CpApplicationAssignedTechnicians")]
    public virtual User? AssignedTechnician { get; set; }

    [ForeignKey("ContractorAddressId")]
    [InverseProperty("CpApplicationContractorAddresses")]
    public virtual Address? ContractorAddress { get; set; }

    [InverseProperty("CpApplication")]
    public virtual ICollection<CpApplicationAssociation> CpApplicationAssociations { get; set; } = new List<CpApplicationAssociation>();

    [ForeignKey("CpStatusId")]
    [InverseProperty("CpApplications")]
    public virtual CpStatus? CpStatus { get; set; }

    [ForeignKey("EwrApplicationId")]
    [InverseProperty("CpApplications")]
    public virtual EwrApplication? EwrApplication { get; set; }

    [ForeignKey("InspFeePayeeAddressId")]
    [InverseProperty("CpApplicationInspFeePayeeAddresses")]
    public virtual Address? InspFeePayeeAddress { get; set; }

    [ForeignKey("IntakeTechnicianId")]
    [InverseProperty("CpApplicationIntakeTechnicians")]
    public virtual User? IntakeTechnician { get; set; }

    [ForeignKey("OwnerAddressId")]
    [InverseProperty("CpApplicationOwnerAddresses")]
    public virtual Address? OwnerAddress { get; set; }

    [ForeignKey("PermiteeAddressId")]
    [InverseProperty("CpApplicationPermiteeAddresses")]
    public virtual Address? PermiteeAddress { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("CpApplications")]
    public virtual Project? Project { get; set; }

    [ForeignKey("PscReviewStatusId")]
    [InverseProperty("CpApplications")]
    public virtual PscReviewStatus? PscReviewStatus { get; set; }

    [ForeignKey("SubmittedBy")]
    [InverseProperty("CpApplicationSubmittedByNavigations")]
    public virtual User? SubmittedByNavigation { get; set; }

    [ForeignKey("WzDepoRefundAddressId")]
    [InverseProperty("CpApplicationWzDepoRefundAddresses")]
    public virtual Address? WzDepoRefundAddress { get; set; }
}
