namespace InsuranceClaims.Application.DTOs;

public class ClaimAssessmentDto
{
    public int Id { get; set; }
    public int ClaimId { get; set; }
    public string AssessorName { get; set; } = string.Empty;
    public DateTime AssessmentDate { get; set; }
    public string Findings { get; set; } = string.Empty;
    public decimal RecommendedAmount { get; set; }
    public bool IsApproved { get; set; }
    public string RejectionReason { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
