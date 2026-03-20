using InsuranceClaims.Application.DTOs;
using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Application.StaticData;

public static class ClaimsDataStore
{
    private static int _nextPolicyHolderId = 4;
    private static int _nextPolicyId = 4;
    private static int _nextClaimId = 4;
    private static int _nextLineItemId = 7;
    private static int _nextAssessmentId = 3;
    private static int _nextSettlementId = 3;

    public static int NextPolicyHolderId() => _nextPolicyHolderId++;
    public static int NextPolicyId() => _nextPolicyId++;
    public static int NextClaimId() => _nextClaimId++;
    public static int NextLineItemId() => _nextLineItemId++;
    public static int NextAssessmentId() => _nextAssessmentId++;
    public static int NextSettlementId() => _nextSettlementId++;

    public static List<PolicyHolderDto> PolicyHolders { get; } = new()
    {
        new PolicyHolderDto
        {
            Id = 1,
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@example.com",
            PhoneNumber = "312-555-0101",
            DateOfBirth = "1985-04-12",
            Address1 = "123 Maple Street",
            City = "Chicago",
            StateCode = "IL",
            ZipCode = "60601",
            Country = "USA",
            IsActive = true,
            Status = PolicyStatus.Active
        },
        new PolicyHolderDto
        {
            Id = 2,
            FirstName = "Bob",
            LastName = "Martinez",
            Email = "bob.martinez@example.com",
            PhoneNumber = "713-555-0202",
            DateOfBirth = "1979-08-23",
            Address1 = "456 Oak Avenue",
            City = "Houston",
            StateCode = "TX",
            ZipCode = "77001",
            Country = "USA",
            IsActive = true,
            Status = PolicyStatus.Active
        },
        new PolicyHolderDto
        {
            Id = 3,
            FirstName = "Carol",
            LastName = "Williams",
            Email = "carol.williams@example.com",
            PhoneNumber = "602-555-0303",
            DateOfBirth = "1992-01-30",
            Address1 = "789 Pine Road",
            City = "Phoenix",
            StateCode = "AZ",
            ZipCode = "85001",
            Country = "USA",
            IsActive = true,
            Status = PolicyStatus.Active
        }
    };

    public static List<InsurancePolicyDto> InsurancePolicies { get; } = new()
    {
        new InsurancePolicyDto
        {
            Id = 1,
            PolicyNumber = "POL-2024-001",
            PolicyType = "Home & Property",
            Description = "Comprehensive home and property insurance coverage.",
            EffectiveDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            PremiumAmount = 1200.00m,
            CoverageAmount = 250000.00m,
            Status = PolicyStatus.Active,
            PolicyHolderId = 1,
            PolicyHolderName = "Alice Johnson"
        },
        new InsurancePolicyDto
        {
            Id = 2,
            PolicyNumber = "POL-2024-002",
            PolicyType = "Auto",
            Description = "Auto insurance covering collision, liability, and comprehensive.",
            EffectiveDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            PremiumAmount = 800.00m,
            CoverageAmount = 50000.00m,
            Status = PolicyStatus.Active,
            PolicyHolderId = 2,
            PolicyHolderName = "Bob Martinez"
        },
        new InsurancePolicyDto
        {
            Id = 3,
            PolicyNumber = "POL-2024-003",
            PolicyType = "Health",
            Description = "Health insurance covering medical expenses and hospitalization.",
            EffectiveDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ExpirationDate = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
            PremiumAmount = 2400.00m,
            CoverageAmount = 100000.00m,
            Status = PolicyStatus.Active,
            PolicyHolderId = 3,
            PolicyHolderName = "Carol Williams"
        }
    };

    public static List<ClaimDto> Claims { get; } = new()
    {
        new ClaimDto
        {
            Id = 1,
            ClaimNumber = "CLM-20240815-A1B2C3D4",
            Description = "Water damage to kitchen and living room caused by burst pipe.",
            IncidentDate = new DateTime(2024, 8, 15, 0, 0, 0, DateTimeKind.Utc),
            SubmittedDate = new DateTime(2024, 8, 16, 0, 0, 0, DateTimeKind.Utc),
            Status = ClaimStatus.Approved,
            TotalClaimedAmount = 18500.00m,
            ApprovedAmount = 17000.00m,
            Notes = "Claim approved following assessment by David Chen.",
            PolicyHolderId = 1,
            InsurancePolicyId = 1,
            PolicyHolderName = "Alice Johnson",
            PolicyNumber = "POL-2024-001"
        },
        new ClaimDto
        {
            Id = 2,
            ClaimNumber = "CLM-20240920-E5F6G7H8",
            Description = "Auto collision on highway resulting in vehicle damage and minor injuries.",
            IncidentDate = new DateTime(2024, 9, 20, 0, 0, 0, DateTimeKind.Utc),
            SubmittedDate = new DateTime(2024, 9, 21, 0, 0, 0, DateTimeKind.Utc),
            Status = ClaimStatus.UnderReview,
            TotalClaimedAmount = 7200.00m,
            ApprovedAmount = 0.00m,
            Notes = string.Empty,
            PolicyHolderId = 2,
            InsurancePolicyId = 2,
            PolicyHolderName = "Bob Martinez",
            PolicyNumber = "POL-2024-002"
        },
        new ClaimDto
        {
            Id = 3,
            ClaimNumber = "CLM-20241105-I9J0K1L2",
            Description = "Emergency appendectomy surgery and hospitalization.",
            IncidentDate = new DateTime(2024, 11, 5, 0, 0, 0, DateTimeKind.Utc),
            SubmittedDate = new DateTime(2024, 11, 6, 0, 0, 0, DateTimeKind.Utc),
            Status = ClaimStatus.Paid,
            TotalClaimedAmount = 22000.00m,
            ApprovedAmount = 20000.00m,
            Notes = "Claim approved and settled by check.",
            PolicyHolderId = 3,
            InsurancePolicyId = 3,
            PolicyHolderName = "Carol Williams",
            PolicyNumber = "POL-2024-003"
        }
    };

    public static List<ClaimLineItemDto> ClaimLineItems { get; } = new()
    {
        new ClaimLineItemDto
        {
            Id = 1,
            ClaimId = 1,
            ItemType = ClaimLineItemType.PropertyDamage,
            Description = "Kitchen structural repair and flooring replacement.",
            ClaimedAmount = 10000.00m,
            ApprovedAmount = 9200.00m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = true,
            Notes = "Approved after contractor assessment."
        },
        new ClaimLineItemDto
        {
            Id = 2,
            ClaimId = 1,
            ItemType = ClaimLineItemType.PropertyDamage,
            Description = "Living room furniture and electronics damaged by water.",
            ClaimedAmount = 8500.00m,
            ApprovedAmount = 7800.00m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = true,
            Notes = "Approved with minor deduction for depreciation."
        },
        new ClaimLineItemDto
        {
            Id = 3,
            ClaimId = 2,
            ItemType = ClaimLineItemType.VehicleDamage,
            Description = "Vehicle front-end damage and airbag deployment.",
            ClaimedAmount = 5500.00m,
            ApprovedAmount = 0.00m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = false,
            Notes = string.Empty
        },
        new ClaimLineItemDto
        {
            Id = 4,
            ClaimId = 2,
            ItemType = ClaimLineItemType.MedicalExpense,
            Description = "Emergency room visit and minor injury treatment.",
            ClaimedAmount = 1700.00m,
            ApprovedAmount = 0.00m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = false,
            Notes = string.Empty
        },
        new ClaimLineItemDto
        {
            Id = 5,
            ClaimId = 3,
            ItemType = ClaimLineItemType.MedicalExpense,
            Description = "Appendectomy surgical procedure costs.",
            ClaimedAmount = 15000.00m,
            ApprovedAmount = 13636.36m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = true,
            Notes = "Approved as part of total settlement."
        },
        new ClaimLineItemDto
        {
            Id = 6,
            ClaimId = 3,
            ItemType = ClaimLineItemType.MedicalExpense,
            Description = "Post-operative hospitalization and recovery care.",
            ClaimedAmount = 7000.00m,
            ApprovedAmount = 6363.64m,
            SupportingDocumentUrl = string.Empty,
            IsApproved = true,
            Notes = "Approved as part of total settlement."
        }
    };

    public static List<ClaimAssessmentDto> ClaimAssessments { get; } = new()
    {
        new ClaimAssessmentDto
        {
            Id = 1,
            ClaimId = 1,
            AssessorName = "David Chen",
            AssessmentDate = new DateTime(2024, 8, 20, 0, 0, 0, DateTimeKind.Utc),
            Findings = "Pipe burst confirmed by plumber report. Damage consistent with claimed amounts.",
            RecommendedAmount = 17000.00m,
            IsApproved = true,
            RejectionReason = string.Empty,
            Notes = "Minor deductions applied for pre-existing wear."
        },
        new ClaimAssessmentDto
        {
            Id = 2,
            ClaimId = 3,
            AssessorName = "Sarah Kim",
            AssessmentDate = new DateTime(2024, 11, 10, 0, 0, 0, DateTimeKind.Utc),
            Findings = "Medical records verified. Appendectomy medically necessary.",
            RecommendedAmount = 20000.00m,
            IsApproved = true,
            RejectionReason = string.Empty,
            Notes = "Partial coverage applied per policy terms."
        }
    };

    public static List<ClaimSettlementDto> ClaimSettlements { get; } = new()
    {
        new ClaimSettlementDto
        {
            Id = 1,
            ClaimId = 1,
            SettlementAmount = 17000.00m,
            SettlementDate = new DateTime(2024, 8, 25, 0, 0, 0, DateTimeKind.Utc),
            PaymentMethod = "Bank Transfer",
            PaymentReference = "BTR-20240825-001",
            Status = SettlementStatus.Paid,
            Notes = "Payment transferred to policyholder's bank account."
        },
        new ClaimSettlementDto
        {
            Id = 2,
            ClaimId = 3,
            SettlementAmount = 20000.00m,
            SettlementDate = new DateTime(2024, 11, 15, 0, 0, 0, DateTimeKind.Utc),
            PaymentMethod = "Check",
            PaymentReference = "CHK-20241115-003",
            Status = SettlementStatus.Paid,
            Notes = "Settlement check mailed to policyholder."
        }
    };
}
