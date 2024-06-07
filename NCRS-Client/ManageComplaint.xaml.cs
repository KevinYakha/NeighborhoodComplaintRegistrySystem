using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using NCRS_API.Data;

namespace NCRS_Client;

/// <summary>
/// Interaction logic for NewComplaint.xaml
/// </summary>
public partial class ManageComplaint : Window
{
    private readonly Client _httpClient = new();
    private Complaint _complaint;

    public ManageComplaint()
    {
        InitializeComponent();

        lb_title.Content = "Add new complaint";

        for (int i = 0; i < (int)Complaint.ComplaintCategory.COUNT; i++)
        {
            cb_category.Items.Add((Complaint.ComplaintCategory)i);
        }
    }

    public ManageComplaint(Complaint complaint)
    {
        InitializeComponent();

        lb_title.Content = "Update a complaint";
        lb_status.Visibility = Visibility.Visible;
        cb_status.Visibility = Visibility.Visible;

        for (int i = 0; i < (int)Complaint.ComplaintStatus.COUNT; i++)
        {
            cb_status.Items.Add((Complaint.ComplaintStatus)i);
        }

        _complaint = complaint;

        tb_first_name.Text = complaint.Issuer.FirstName;
        tb_last_name.Text = complaint.Issuer.LastName;
        tb_apartment_nr.Text = complaint.ComplaintLocation.ApartmentNr.ToString();
        tb_description.Text = complaint.Description;
        cb_category.SelectedIndex = (int)complaint.Category;
        cb_status.SelectedIndex = (int)complaint.Status;

        tb_first_name.IsEnabled = false;
        tb_last_name.IsEnabled = false;
        tb_apartment_nr.IsEnabled = false;
        cb_category.IsEnabled = false;

        for (int i = 0; i < (int)Complaint.ComplaintCategory.COUNT; i++)
        {
            cb_category.Items.Add((Complaint.ComplaintCategory)i);
        }
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

    private void tb_apartment_nr_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private async void bt_submit_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Tuple<TextBlock, HttpStatusCode>> statusLine = new()
            {
                new(tb_submit_success, HttpStatusCode.OK),
                new(tb_submit_failure, HttpStatusCode.NoContent),
                new(tb_submit_failure, HttpStatusCode.RequestTimeout)
            };

            HttpResponseMessage response = new();

            if (!(_complaint.Id == null))
            {
                _complaint.Description = tb_description.Text;
                _complaint.Status = (Complaint.ComplaintStatus)cb_status.SelectedIndex;

                response = await _httpClient.SubmitComplaintUpdateAsync(_complaint);
            }
            else if (_complaint.Id == null)
            {
                Tenant issuer = new()
                {
                    FirstName = tb_first_name.Text,
                    LastName = tb_last_name.Text
                };

                response = await _httpClient.FindTenantByNameAsync(issuer);

                if (!response.IsSuccessStatusCode)
                {
                    AdjustStatusLine(statusLine, response.StatusCode);
                    return;
                }

                issuer = await response.Content.ReadFromJsonAsync<Tenant>();

                _complaint = new()
                {
                    Issuer = issuer,
                    ComplaintLocation = new() { ApartmentNr = int.Parse(tb_apartment_nr.Text) },
                    Description = tb_description.Text,
                    Category = (Complaint.ComplaintCategory)cb_category.SelectedIndex,
                    Status = Complaint.ComplaintStatus.Pending
                };

                response = await _httpClient.SubmitNewComplaintAsync(_complaint);
            }
            else
            {
                throw new ArgumentException();
            }

            AdjustStatusLine(statusLine, response.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void bt_find_issuer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Tuple<TextBlock, HttpStatusCode>> statusLine = new()
            {
                new(tb_issuer_found, HttpStatusCode.OK),
                new(tb_issuer_notfound, HttpStatusCode.NoContent),
                new(tb_issuer_connectionfailure, HttpStatusCode.RequestTimeout)
            };

            Tenant tenant = new()
            {
                FirstName = tb_first_name.Text,
                LastName = tb_last_name.Text
            };

            HttpResponseMessage response = await _httpClient.FindTenantByNameAsync(tenant);

            AdjustStatusLine(statusLine, response.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void bt_find_location_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Tuple<TextBlock, HttpStatusCode>> statusLine = new()
            {
                new(tb_location_found, HttpStatusCode.OK),
                new(tb_location_notfound, HttpStatusCode.NoContent),
                new(tb_location_connectionfailure, HttpStatusCode.RequestTimeout)
            };

            if (!int.TryParse(tb_apartment_nr.Text, out int apartmentNr))
            {
                tb_location_connectionfailure.Visibility = Visibility.Hidden;
                tb_location_found.Visibility = Visibility.Hidden;
                tb_location_notfound.Visibility = Visibility.Visible;
                return;
            }
            Apartment apartment = new()
            {
                ApartmentNr = apartmentNr
            };

            HttpResponseMessage response = await _httpClient.FindApartment(apartment);

            AdjustStatusLine(statusLine, response.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
