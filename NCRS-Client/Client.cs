using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

using NCRS_Data;

namespace NCRS_Client;

class Client : HttpClient
{
    private string _uri = "https://localhost:7103/NCRS/";

    public async Task<HttpResponseMessage> SubmitNewComplaintAsync(Complaint newComplaint)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "CreateNewComplaint");

            return await http.PostAsJsonAsync(uri, newComplaint);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    internal async Task<HttpResponseMessage> SubmitComplaintUpdateAsync(Complaint complaint)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "UpdateComplaintData");

            return await http.PutAsJsonAsync(uri, complaint);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> FindTenantByNameAsync(Tenant tenant)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveTenantByName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Content = JsonContent.Create(tenant);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> FindApartment(Apartment apartment)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveApartment");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Content = JsonContent.Create(apartment);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveAllComplaints()
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaints");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Headers.Add("Token", "EmployeeToken");

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByName(Tenant issuer)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Content = JsonContent.Create(issuer);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByPartialName(Tenant issuer)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByPartialName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Headers.Add("Token", "EmployeeToken");
            message.Content = JsonContent.Create(issuer);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByWildcardName(Tenant issuer)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByWildcardName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Headers.Add("Token", "EmployeeToken");
            message.Content = JsonContent.Create(issuer);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByDate(DateTime date)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByDate");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Headers.Add("Token", "EmployeeToken");
            message.Content = JsonContent.Create(date);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByDateAndName(DateTime date, Tenant issuer)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByDateAndName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            Tuple<DateTime, Tenant> dataTuple = new(date, issuer);
            message.Content = JsonContent.Create(dataTuple);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByDateRange(Tuple<DateTime, DateTime> dateRange)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByDateRange");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Headers.Add("Token", "EmployeeToken");
            message.Content = JsonContent.Create(dateRange);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<HttpResponseMessage> RetrieveComplaintsByDateRangeAndName(Tuple<DateTime, DateTime> dateRange, Tenant issuer)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri + "RetrieveComplaintsByDateRangeAndName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            Tuple<Tuple<DateTime, DateTime>, Tenant> dataTuple = new(dateRange, issuer);
            message.Content = JsonContent.Create(dataTuple);

            return await http.SendAsync(message);
        }
        catch (HttpRequestException)
        {
            return new(HttpStatusCode.RequestTimeout);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
