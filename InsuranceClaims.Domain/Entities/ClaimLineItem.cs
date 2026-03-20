using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Domain.Entities;

public class ClaimLineItem
{
    public int Id { get; set; }
    public int ClaimId { get; set; }
    public ClaimLineItemType ItemType { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal ClaimedAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public string SupportingDocumentUrl { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
    public string Notes { get; set; } = string.Empty;
}
