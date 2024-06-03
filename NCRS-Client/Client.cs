using System.Net.Http;
using System.Net.Http.Json;

using NCRS_API.Data;

namespace NCRS_Client;

class Client : HttpClient
{
    public async static Task<HttpResponseMessage> SubmitNewComplaintAsync(Complaint newComplaint)
    {
        try
        {
            HttpClient http = new();
            Uri uri = new(new("https://localhost:port"), "CreateNewComplaint");

            HttpRequestMessage message = new(HttpMethod.Post, uri);
            message.Content = JsonContent.Create(newComplaint);

            return await http.SendAsync(message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
