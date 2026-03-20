using InsuranceClaims.Domain.Enums;

namespace InsuranceClaims.Domain.Entities;

public class PolicyHolder
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StateCode { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public PolicyStatus Status { get; set; }
}
