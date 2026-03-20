using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Domain.Entities;

public class InsurancePolicy
{
    public int Id { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string PolicyType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public decimal PremiumAmount { get; set; }
    public decimal CoverageAmount { get; set; }
    public PolicyStatus Status { get; set; }
    public int PolicyHolderId { get; set; }
}
