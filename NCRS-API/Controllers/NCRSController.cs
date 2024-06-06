using Microsoft.AspNetCore.Mvc;

using NCRS_API.Data;

namespace NCRS_API.Controllers;

[Route("[Controller]/[Action]")]
public class NCRSController : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateNewComplaint([FromBody]Complaint newComplaint)
    {
        try
        {
            return Ok(await NCRS_DB.InsertNewComplaintAsync(newComplaint));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComplaintData([FromBody]Complaint updatedComplaintData)
    {
        try
        {
            Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaintAsync(updatedComplaintData.Id);
            complaintToUpdate.Description = updatedComplaintData.Description;

            return Ok(await NCRS_DB.UpdateComplaintAsync(complaintToUpdate));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByName([FromQuery] string firstName, [FromQuery] string lastName)
    {
        try
        {
            Tenant tenant = new()
            {
                FirstName = firstName,
                LastName = lastName
            };

            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsByNameAsync(tenant);
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveComplaint([FromQuery]string Id)
    {
        try
        {
            Complaint retrievedComplaint = await NCRS_DB.RetrieveComplaintAsync(Id);
            if (retrievedComplaint.Id == null)
            {
                return NoContent();
            }
            return Ok(retrievedComplaint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveTenantByName([FromBody]Tenant tenant)
    {
        try
        {
            Tenant retrievedTenant = await NCRS_DB.RetrieveTenantByNameAsync(tenant);
            if (retrievedTenant.Id == null)
            {
                return NoContent();
            }
            return Ok(retrievedTenant);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveApartment([FromBody]Apartment apartment)
    {
        try
        {
            Apartment retrievedApartment = await NCRS_DB.RetrieveApartmentAsync(apartment.ApartmentNr);
            if (retrievedApartment.ApartmentNr == null)
            {
                return NoContent();
            }
            return Ok(retrievedApartment);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
    [HttpPut]
    public async Task<IActionResult> UpdateComplaintStatus([FromBody]Complaint updatedComplaintStatus)
    {
        try
        {
            Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaintAsync(updatedComplaintStatus.Id);
            complaintToUpdate.Status = updatedComplaintStatus.Status;

            return Ok(await NCRS_DB.UpdateComplaintAsync(complaintToUpdate));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByDate([FromBody]DateTime date)
    {
        try
        {
            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsByDateAsync(date);
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByDateAndName([FromBody]Tuple<DateTime, Tenant> dateAndTime)
    {
        try
        {
            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsByDateAndNameAsync(dateAndTime.Item1, dateAndTime.Item2);
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByDateRange([FromBody]Tuple<DateTime, DateTime> dateRange)
    {
        try
        {
            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsByDateRangeAsync(dateRange);
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByDateRangeAndName([FromBody]Tuple<Tuple<DateTime, DateTime>, Tenant> dateRangeAndIssuer)
    {
        try
        {
            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsByDateRangeAndNameAsync(dateRangeAndIssuer.Item1, dateRangeAndIssuer.Item2);
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<IActionResult> RetrieveComplaints()
    {
        try
        {
            List<Complaint> retrievedComplaints = await NCRS_DB.RetrieveComplaintsAsync();
            if (retrievedComplaints == null || retrievedComplaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(retrievedComplaints);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }
}
