using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
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

        public static void AdjustStatusLine(List<Tuple<TextBlock, HttpStatusCode>> textBlockStatusTuple, HttpStatusCode statusCode)
        {
            foreach (Tuple<TextBlock, HttpStatusCode> statusTuple in textBlockStatusTuple)
            {
                if (statusTuple.Item2 == statusCode)
                {
                    statusTuple.Item1.Visibility = Visibility.Visible;
                }
                else
                {
                    statusTuple.Item1.Visibility = Visibility.Hidden;
                }
            }
        }

        private async void CreateComplaintTableData()
        {
            try
            {
                List<Tuple<TextBlock, HttpStatusCode>> statusLine = new()
                {
                    new(tb_loading_content, HttpStatusCode.Processing),
                    new(tb_loading_failure, HttpStatusCode.NoContent),
                    new(tb_loading_timeout, HttpStatusCode.RequestTimeout)
                };

                HttpResponseMessage response = await _httpClient.RetrieveAllComplaints();

                if (!response.IsSuccessStatusCode)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                List<Complaint> complaints = await response.Content.ReadFromJsonAsync<List<Complaint>>();

                ic_complaint_entry.ItemsSource = complaints;

                AdjustStatusLine(statusLine, response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
