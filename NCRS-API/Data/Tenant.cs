namespace NCRS_API.Data;

public struct Tenant
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Apartment Residence { get; set; }
}
