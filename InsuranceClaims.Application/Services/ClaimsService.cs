using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using InsuranceClaims.Application.StaticData;
using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Application.Services;

public class ClaimsService : IClaimsService
{
    private ClaimDto EnrichClaim(ClaimDto claim)
    {
        claim.LineItems = ClaimsDataStore.ClaimLineItems
            .Where(li => li.ClaimId == claim.Id)
            .ToList();
        claim.Assessment = ClaimsDataStore.ClaimAssessments
            .FirstOrDefault(a => a.ClaimId == claim.Id);
        claim.Settlement = ClaimsDataStore.ClaimSettlements
            .FirstOrDefault(s => s.ClaimId == claim.Id);
        return claim;
    }

    public List<ClaimDto> GetAll()
    {
        return ClaimsDataStore.Claims.Select(EnrichClaim).ToList();
    }

    public ClaimDto? GetById(int id)
    {
        var claim = ClaimsDataStore.Claims.FirstOrDefault(c => c.Id == id);
        return claim is null ? null : EnrichClaim(claim);
    }

    public List<ClaimDto> GetByPolicyHolderId(int policyHolderId)
    {
        return ClaimsDataStore.Claims
            .Where(c => c.PolicyHolderId == policyHolderId)
            .Select(EnrichClaim)
            .ToList();
    }

    public List<ClaimDto> GetByPolicyId(int policyId)
    {
        return ClaimsDataStore.Claims
            .Where(c => c.InsurancePolicyId == policyId)
            .Select(EnrichClaim)
            .ToList();
    }

    public ClaimDto Submit(ClaimDto dto)
    {
        var policy = ClaimsDataStore.InsurancePolicies.FirstOrDefault(p => p.Id == dto.InsurancePolicyId)
            ?? throw new KeyNotFoundException($"InsurancePolicy with Id {dto.InsurancePolicyId} not found.");

        if (policy.Status != PolicyStatus.Active)
            throw new InvalidOperationException($"Policy {policy.PolicyNumber} is not Active.");

        if (dto.Id > 0)
        {
            var existing = ClaimsDataStore.Claims.FirstOrDefault(c => c.Id == dto.Id)
                ?? throw new KeyNotFoundException($"Claim with Id {dto.Id} not found.");

            if (existing.Status != ClaimStatus.Draft)
                throw new InvalidOperationException("Only claims in Draft status can be updated.");

            existing.Description = dto.Description;
            existing.IncidentDate = dto.IncidentDate;
            existing.Notes = dto.Notes;
            existing.TotalClaimedAmount = dto.TotalClaimedAmount;

            return EnrichClaim(existing);
        }
        else
        {
            var holder = ClaimsDataStore.PolicyHolders.FirstOrDefault(p => p.Id == dto.PolicyHolderId)
                ?? throw new KeyNotFoundException($"PolicyHolder with Id {dto.PolicyHolderId} not found.");

            dto.Id = ClaimsDataStore.NextClaimId();
            dto.ClaimNumber = $"CLM-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            dto.Status = ClaimStatus.Submitted;
            dto.SubmittedDate = DateTime.UtcNow;
            dto.ApprovedAmount = 0;
            dto.PolicyHolderName = holder.FullName;
            dto.PolicyNumber = policy.PolicyNumber;

            ClaimsDataStore.Claims.Add(dto);
            return EnrichClaim(dto);
        }
    }

    public ClaimLineItemDto AddLineItem(ClaimLineItemDto dto)
    {
        var claim = ClaimsDataStore.Claims.FirstOrDefault(c => c.Id == dto.ClaimId)
            ?? throw new KeyNotFoundException($"Claim with Id {dto.ClaimId} not found.");

        if (claim.Status != ClaimStatus.Submitted && claim.Status != ClaimStatus.UnderReview)
            throw new InvalidOperationException("Line items can only be added to claims in Submitted or UnderReview status.");

        if (dto.Id > 0)
        {
            var existing = ClaimsDataStore.ClaimLineItems.FirstOrDefault(li => li.Id == dto.Id)
                ?? throw new KeyNotFoundException($"ClaimLineItem with Id {dto.Id} not found.");

            var delta = dto.ClaimedAmount - existing.ClaimedAmount;

            existing.ItemType = dto.ItemType;
            existing.Description = dto.Description;
            existing.ClaimedAmount = dto.ClaimedAmount;
            existing.ApprovedAmount = dto.ApprovedAmount;
            existing.SupportingDocumentUrl = dto.SupportingDocumentUrl;
            existing.IsApproved = dto.IsApproved;
            existing.Notes = dto.Notes;

            claim.TotalClaimedAmount += delta;

            return existing;
        }
        else
        {
            dto.Id = ClaimsDataStore.NextLineItemId();
            dto.IsApproved = false;
            dto.ApprovedAmount = 0;

            ClaimsDataStore.ClaimLineItems.Add(dto);
            claim.TotalClaimedAmount += dto.ClaimedAmount;

            return dto;
        }
    }

    public ClaimDto Assess(ClaimAssessmentDto dto)
    {
        var claim = ClaimsDataStore.Claims.FirstOrDefault(c => c.Id == dto.ClaimId)
            ?? throw new KeyNotFoundException($"Claim with Id {dto.ClaimId} not found.");

        if (claim.Status != ClaimStatus.Submitted &&
            claim.Status != ClaimStatus.UnderReview &&
            claim.Status != ClaimStatus.AssessmentInProgress)
        {
            throw new InvalidOperationException("Claim must be in Submitted, UnderReview, or AssessmentInProgress status to be assessed.");
        }

        if (dto.AssessmentDate == default)
            dto.AssessmentDate = DateTime.UtcNow;

        var existingAssessment = ClaimsDataStore.ClaimAssessments.FirstOrDefault(a => a.ClaimId == dto.ClaimId);
        if (existingAssessment is not null)
        {
            existingAssessment.AssessorName = dto.AssessorName;
            existingAssessment.AssessmentDate = dto.AssessmentDate;
            existingAssessment.Findings = dto.Findings;
            existingAssessment.RecommendedAmount = dto.RecommendedAmount;
            existingAssessment.IsApproved = dto.IsApproved;
            existingAssessment.RejectionReason = dto.RejectionReason;
            existingAssessment.Notes = dto.Notes;
        }
        else
        {
            dto.Id = ClaimsDataStore.NextAssessmentId();
            ClaimsDataStore.ClaimAssessments.Add(dto);
        }

        var lineItems = ClaimsDataStore.ClaimLineItems.Where(li => li.ClaimId == dto.ClaimId).ToList();

        if (dto.IsApproved)
        {
            claim.Status = ClaimStatus.Approved;
            claim.ApprovedAmount = dto.RecommendedAmount;

            if (lineItems.Count > 0)
            {
                decimal total = lineItems.Sum(li => li.ClaimedAmount);
                decimal distributed = 0m;

                for (int i = 0; i < lineItems.Count - 1; i++)
                {
                    var ratio = total > 0 ? lineItems[i].ClaimedAmount / total : 0m;
                    var approvedForItem = Math.Round(dto.RecommendedAmount * ratio, 2);
                    lineItems[i].ApprovedAmount = approvedForItem;
                    lineItems[i].IsApproved = true;
                    distributed += approvedForItem;
                }

                // Last item gets the remainder to avoid rounding drift
                lineItems[^1].ApprovedAmount = dto.RecommendedAmount - distributed;
                lineItems[^1].IsApproved = true;
            }
        }
        else
        {
            claim.Status = ClaimStatus.Rejected;
            claim.ApprovedAmount = 0;

            foreach (var item in lineItems)
            {
                item.IsApproved = false;
                item.ApprovedAmount = 0;
            }
        }

        return EnrichClaim(claim);
    }

    public ClaimAssessmentDto? GetAssessment(int claimId)
    {
        return ClaimsDataStore.ClaimAssessments.FirstOrDefault(a => a.ClaimId == claimId);
    }
}
