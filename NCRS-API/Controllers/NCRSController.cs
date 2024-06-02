using Microsoft.AspNetCore.Http.HttpResults;
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
            return Ok(await NCRS_DB.InsertNewComplaint(newComplaint));
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
            Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaint(updatedComplaintData.Id);
            complaintToUpdate.Description = updatedComplaintData.Description;

            return Ok(await NCRS_DB.UpdateComplaint(complaintToUpdate));
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
            Complaint complaintToUpdate = await NCRS_DB.RetrieveComplaint(updatedComplaintStatus.Id);
            complaintToUpdate.Status = updatedComplaintStatus.Status;

            return Ok(await NCRS_DB.UpdateComplaint(complaintToUpdate));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
    [HttpGet]
    public async Task<IActionResult> RetrieveComplaintsByDate([FromQuery]string date)
    {
        try
        {
            return Ok(await NCRS_DB.RetrieveComplaintsByDate(DateTime.Parse(date)));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }

    [AuthorizeEmployee]
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

            return Ok(await NCRS_DB.RetrieveComplaintsByName(tenant));
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
            return Ok(await NCRS_DB.RetrieveComplaints());
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
            return Ok(await NCRS_DB.RetrieveComplaint(Id));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR START\n{ex}\nERROR END");
            return BadRequest(ex);
        }
    }
}
