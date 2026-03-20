using InsuranceClaims.Application.DTOs;

namespace InsuranceClaims.Application.Interfaces;

public interface IInsurancePolicyService
{
    InsurancePolicyDto? GetById(int id);
    List<InsurancePolicyDto> GetByHolderId(int policyHolderId);
    List<ClaimDto> GetClaims(int policyId);
    InsurancePolicyDto CreateOrUpdate(InsurancePolicyDto dto);
}
