using InsuranceClaims.Application.Interfaces;
using InsuranceClaims.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceClaims.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInsuranceClaimsServices(this IServiceCollection services)
    {
        services.AddSingleton<IPolicyHolderService, PolicyHolderService>();
        services.AddSingleton<IInsurancePolicyService, InsurancePolicyService>();
        services.AddSingleton<IClaimsService, ClaimsService>();
        services.AddSingleton<IClaimSettlementService, ClaimSettlementService>();

        return services;
    }
}
