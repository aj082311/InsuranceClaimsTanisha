namespace InsuranceClaims.Domain.Enums;

public enum ClaimStatus
{
    Draft = 1,
    Submitted = 2,
    UnderReview = 3,
    AssessmentInProgress = 4,
    Approved = 5,
    Rejected = 6,
    SettlementInProgress = 7,
    Paid = 8,
    Closed = 9,
    Cancelled = 10
}
