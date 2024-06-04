using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

using NCRS_API.Data;

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
}
