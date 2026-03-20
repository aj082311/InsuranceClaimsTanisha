using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using InsuranceClaims.Application.StaticData;
using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Application.Services;

public class PolicyHolderService : IPolicyHolderService
{
    public List<PolicyHolderDto> GetAll()
    {
        return ClaimsDataStore.PolicyHolders;
    }

    public PolicyHolderDto? GetById(int id)
    {
        return ClaimsDataStore.PolicyHolders.FirstOrDefault(p => p.Id == id);
    }

    public List<InsurancePolicyDto> GetPolicies(int policyHolderId)
    {
        return ClaimsDataStore.InsurancePolicies
            .Where(p => p.PolicyHolderId == policyHolderId)
            .ToList();
    }

    public List<ClaimDto> GetClaims(int policyHolderId)
    {
        return ClaimsDataStore.Claims
            .Where(c => c.PolicyHolderId == policyHolderId)
            .ToList();
    }

    public PolicyHolderDto CreateOrUpdate(PolicyHolderDto dto)
    {
        if (dto.Id > 0)
        {
            var existing = ClaimsDataStore.PolicyHolders.FirstOrDefault(p => p.Id == dto.Id)
                ?? throw new KeyNotFoundException($"PolicyHolder with Id {dto.Id} not found.");

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.Address1 = dto.Address1;
            existing.City = dto.City;
            existing.StateCode = dto.StateCode;
            existing.ZipCode = dto.ZipCode;
            existing.Country = dto.Country;
            existing.IsActive = dto.IsActive;
            existing.Status = dto.Status;

            return existing;
        }
        else
        {
            dto.Id = ClaimsDataStore.NextPolicyHolderId();
            dto.IsActive = true;
            dto.Status = PolicyStatus.Active;
            ClaimsDataStore.PolicyHolders.Add(dto);
            return dto;
        }
    }
}
