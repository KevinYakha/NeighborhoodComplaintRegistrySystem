using Microsoft.AspNetCore.Mvc;

using NCRS_API.Data;

namespace NCRS_API.Controllers;

[Route("[Controller]/[Action]")]
public class NCRSController : Controller
{
    [HttpPost]
    public async Task<int> CreateNewComplaint([FromBody]Complaint newComplaint)
    {
        return await NCRS_DB.InsertNewComplaint(newComplaint);
    }

    [HttpPost]
    public async Task<int> CreateNewComplaint([FromBody]Dictionary<string, object> newComplaint)
    {
        Complaint constructedComplaint = new()
        {
            Issuer = new Tenant() { Id = newComplaint["Issuer"].ToString() },
            ComplaintLocation = new Apartment() { ApartmentNr = int.Parse(newComplaint["Location"].ToString()) },
            Description = newComplaint["Description"].ToString(),
            Status = (Complaint.ComplaintStatus)int.Parse(newComplaint["Status"].ToString()),
            Category = (Complaint.ComplaintCategory)int.Parse(newComplaint["Category"].ToString())
        };
        return await NCRS_DB.InsertNewComplaint(constructedComplaint);
    }

    [HttpPut]
    public async Task<int> UpdateComplaintData([FromBody]Dictionary<string, string> updatedComplaintData)
    {
        Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaint(updatedComplaintData["Id"]);
        complaintToUpdate.Description = updatedComplaintData["Description"];

        return await NCRS_DB.UpdateComplaint(complaintToUpdate);
    }

    [AuthorizeEmployee]
    [HttpPut]
    public async Task<int> UpdateComplaintStatus([FromBody]Dictionary<string, object> updatedComplaintData)
    {
        Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaint(updatedComplaintData["Id"].ToString());
        complaintToUpdate.Status = (Complaint.ComplaintStatus)int.Parse(updatedComplaintData["Status"].ToString());

        return await NCRS_DB.UpdateComplaint(complaintToUpdate);
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<List<Complaint>> RetrieveComplaintsByDate([FromQuery]string date)
    {
        return await NCRS_DB.RetrieveComplaintsByDate(DateTime.Parse(date));
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<List<Complaint>> RetrieveComplaintsByName([FromQuery]string fullName)
    {
        string[] nameParts = fullName.Split(' ');

        string lastName = nameParts[^1];
        string firstName = "";

        for (int i = 0; i < nameParts.Length - 1; i++)
        {
            firstName += nameParts[i];
        }

        Tenant tenant = new()
        {
            FirstName = firstName,
            LastName = lastName
        };

        return await NCRS_DB.RetrieveComplaintsByName(tenant);
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<List<Complaint>> RetrieveComplaints()
    {
        return await NCRS_DB.RetrieveComplaints();
    }

    [HttpGet]
    public async Task<Complaint> RetrieveComplaint([FromQuery]string Id)
    {
        return await NCRS_DB.RetrieveComplaint(Id);
    }
}
