using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using InsuranceClaims.Application.StaticData;

namespace InsuranceClaims.Application.Services;

public class InsurancePolicyService : IInsurancePolicyService
{
    public InsurancePolicyDto? GetById(int id)
    {
        return ClaimsDataStore.InsurancePolicies.FirstOrDefault(p => p.Id == id);
    }

    public List<InsurancePolicyDto> GetByHolderId(int policyHolderId)
    {
        return ClaimsDataStore.InsurancePolicies
            .Where(p => p.PolicyHolderId == policyHolderId)
            .ToList();
    }

    public List<ClaimDto> GetClaims(int policyId)
    {
        return ClaimsDataStore.Claims
            .Where(c => c.InsurancePolicyId == policyId)
            .ToList();
    }

    public InsurancePolicyDto CreateOrUpdate(InsurancePolicyDto dto)
    {
        var holder = ClaimsDataStore.PolicyHolders.FirstOrDefault(p => p.Id == dto.PolicyHolderId)
            ?? throw new KeyNotFoundException($"PolicyHolder with Id {dto.PolicyHolderId} not found.");

        if (dto.Id > 0)
        {
            var existing = ClaimsDataStore.InsurancePolicies.FirstOrDefault(p => p.Id == dto.Id)
                ?? throw new KeyNotFoundException($"InsurancePolicy with Id {dto.Id} not found.");

            existing.PolicyNumber = dto.PolicyNumber;
            existing.PolicyType = dto.PolicyType;
            existing.Description = dto.Description;
            existing.EffectiveDate = dto.EffectiveDate;
            existing.ExpirationDate = dto.ExpirationDate;
            existing.PremiumAmount = dto.PremiumAmount;
            existing.CoverageAmount = dto.CoverageAmount;
            existing.Status = dto.Status;
            existing.PolicyHolderId = dto.PolicyHolderId;
            existing.PolicyHolderName = holder.FullName;

            return existing;
        }
        else
        {
            dto.Id = ClaimsDataStore.NextPolicyId();
            dto.PolicyHolderName = holder.FullName;
            ClaimsDataStore.InsurancePolicies.Add(dto);
            return dto;
        }
    }
}
