namespace NCRS_API.Data;

public struct Complaint
{
    public enum ComplaintStatus
    {
        Pending,
        Active,
        Resolved,
        Cancelled,
        COUNT
    }
    public enum ComplaintCategory
    {
        Noise,
        TenantConflict,
        MaintenanceRequest,
        SafetyConcerns,
        PrivacyViolation,
        RecyclingViolation,
        ParkingViolation,
        PetPolicy,
        PestIssues,
        COUNT
    }

    public string Id { get; set; }
    public Tenant Issuer { get; set; }
    public Apartment ComplaintLocation { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public ComplaintStatus Status { get; set; }
    public ComplaintCategory Category { get; set; }
}
