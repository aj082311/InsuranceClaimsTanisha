using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Domain.Entities;

public class ClaimSettlement
{
    public int Id { get; set; }
    public int ClaimId { get; set; }
    public decimal SettlementAmount { get; set; }
    public DateTime? SettlementDate { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentReference { get; set; } = string.Empty;
    public SettlementStatus Status { get; set; }
    public string Notes { get; set; } = string.Empty;
}
