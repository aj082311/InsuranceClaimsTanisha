using InsuranceClaims.Application.DTOs;

namespace InsuranceClaims.Application.Interfaces;

public interface IPolicyHolderService
{
    List<PolicyHolderDto> GetAll();
    PolicyHolderDto? GetById(int id);
    List<InsurancePolicyDto> GetPolicies(int policyHolderId);
    List<ClaimDto> GetClaims(int policyHolderId);
    PolicyHolderDto CreateOrUpdate(PolicyHolderDto dto);
}
