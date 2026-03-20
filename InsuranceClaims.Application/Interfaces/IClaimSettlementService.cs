using InsuranceClaims.Application.DTOs;

namespace InsuranceClaims.Application.Interfaces;

public interface IClaimSettlementService
{
    ClaimSettlementDto? GetByClaimId(int claimId);
    ClaimSettlementDto Process(ClaimSettlementDto dto);
}
