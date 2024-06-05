using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Controls;

using NCRS_API.Data;

namespace NCRS_Client
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Page
    {
        private readonly Client _httpClient = new();

        public Overview()
        {
            InitializeComponent();

            CreateComplaintTableData();
        }

        private async void CreateComplaintTableData()
        {
            try
            {
                /* TODO: Implement status line
                List<Tuple<TextBlock, HttpStatusCode>> statusLine = new()
                {
                    new(success, HttpStatusCode.OK),
                    new(failure, HttpStatusCode.NoContent),
                    new(connectionFailure, HttpStatusCode.RequestTimeout)
                };
                */

                HttpResponseMessage response = await _httpClient.RetrieveAllComplaints();
                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();

                ic_complaint_entry.ItemsSource = complaints;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
