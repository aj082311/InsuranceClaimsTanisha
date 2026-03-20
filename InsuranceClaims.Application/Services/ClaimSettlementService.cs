using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Application.Interfaces;
using InsuranceClaims.Application.StaticData;
using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Application.Services;

public class ClaimSettlementService : IClaimSettlementService
{
    public ClaimSettlementDto? GetByClaimId(int claimId)
    {
        return ClaimsDataStore.ClaimSettlements.FirstOrDefault(s => s.ClaimId == claimId);
    }

    public ClaimSettlementDto Process(ClaimSettlementDto dto)
    {
        var claim = ClaimsDataStore.Claims.FirstOrDefault(c => c.Id == dto.ClaimId)
            ?? throw new KeyNotFoundException($"Claim with Id {dto.ClaimId} not found.");

        if (claim.Status != ClaimStatus.Approved)
            throw new InvalidOperationException("Only approved claims can be settled.");

        var existing = ClaimsDataStore.ClaimSettlements.FirstOrDefault(s => s.ClaimId == dto.ClaimId);

        if (existing is not null)
        {
            if (existing.Status == SettlementStatus.Paid)
                throw new InvalidOperationException("Settlement has already been paid.");

            existing.SettlementAmount = dto.SettlementAmount > 0 ? dto.SettlementAmount : claim.ApprovedAmount;
            existing.PaymentMethod = dto.PaymentMethod;
            existing.PaymentReference = dto.PaymentReference;
            existing.Status = dto.Status;
            existing.Notes = dto.Notes;

            if (dto.Status == SettlementStatus.Paid)
            {
                existing.SettlementDate = DateTime.UtcNow;
                claim.Status = ClaimStatus.Paid;
            }
            else if (dto.Status == SettlementStatus.Processing)
            {
                claim.Status = ClaimStatus.SettlementInProgress;
            }

            return existing;
        }
        else
        {
            var newSettlement = new ClaimSettlementDto
            {
                Id = ClaimsDataStore.NextSettlementId(),
                ClaimId = dto.ClaimId,
                SettlementAmount = dto.SettlementAmount > 0 ? dto.SettlementAmount : claim.ApprovedAmount,
                PaymentMethod = dto.PaymentMethod,
                PaymentReference = dto.PaymentReference,
                Status = SettlementStatus.Processing,
                Notes = dto.Notes
            };

            claim.Status = ClaimStatus.SettlementInProgress;
            ClaimsDataStore.ClaimSettlements.Add(newSettlement);
            return newSettlement;
        }
    }
}
