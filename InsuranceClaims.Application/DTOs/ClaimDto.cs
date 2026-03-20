using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Application.DTOs;

public class ClaimDto
{
    public int Id { get; set; }
    public string ClaimNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; }
    public DateTime SubmittedDate { get; set; }
    public ClaimStatus Status { get; set; }
    public decimal TotalClaimedAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int PolicyHolderId { get; set; }
    public int InsurancePolicyId { get; set; }
    public string PolicyHolderName { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public List<ClaimLineItemDto> LineItems { get; set; } = new();
    public ClaimAssessmentDto? Assessment { get; set; }
    public ClaimSettlementDto? Settlement { get; set; }
}
