using InsuranceClaims.Application.DTOs;

namespace InsuranceClaims.Application.Interfaces;

public interface IClaimsService
{
    List<ClaimDto> GetAll();
    ClaimDto? GetById(int id);
    List<ClaimDto> GetByPolicyHolderId(int policyHolderId);
    List<ClaimDto> GetByPolicyId(int policyId);
    ClaimDto Submit(ClaimDto dto);
    ClaimLineItemDto AddLineItem(ClaimLineItemDto dto);
    ClaimDto Assess(ClaimAssessmentDto dto);
    ClaimAssessmentDto? GetAssessment(int claimId);
}
