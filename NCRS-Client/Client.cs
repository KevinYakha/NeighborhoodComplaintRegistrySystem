using System.Net.Http;
using System.Net.Http.Json;

using NCRS_API.Data;

namespace NCRS_Client;

class Client : HttpClient
{
    private Uri _uri = new("https://localhost:port");

    public async Task<HttpResponseMessage> SubmitNewComplaintAsync(Complaint newComplaint)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(_uri, "CreateNewComplaint");

            HttpRequestMessage message = new(HttpMethod.Post, uri);
            message.Content = JsonContent.Create(newComplaint);

            return await http.SendAsync(message);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Tenant> FindTenantByNameAsync(Tenant tenant)
    {

        try
        {
            HttpClient http = new();
            Uri uri = new(_uri, "RetrieveTenantByName");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Content = JsonContent.Create(tenant);
            HttpResponseMessage response = await http.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tenant>();
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Apartment> FindApartment(Apartment apartment)
    {

        try
        {
            HttpClient http = new();
            Uri uri = new(_uri, "RetrieveApartment");

            HttpRequestMessage message = new(HttpMethod.Get, uri);
            message.Content = JsonContent.Create(apartment);
            HttpResponseMessage response = await http.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Apartment>();
            }

            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
