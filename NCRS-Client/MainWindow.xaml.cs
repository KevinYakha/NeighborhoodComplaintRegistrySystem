using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;

using NCRS_API.Data;

namespace NCRS_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Client _httpClient = new();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < (int)Complaint.ComplaintCategory.COUNT; i++)
            {
                cb_category.Items.Add((Complaint.ComplaintCategory)i);
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
                Tenant issuer = new()
                {
                    FirstName = tb_first_name.Text,
                    LastName = tb_last_name.Text
                };
                issuer = await _httpClient.FindTenantByNameAsync(issuer);

                Complaint newComplaint = new()
                {
                    Issuer = issuer,
                    ComplaintLocation = new() { ApartmentNr = int.Parse(tb_apartment_nr.Text) },
                    Description = tb_description.Text,
                    Category = (Complaint.ComplaintCategory)cb_category.SelectedIndex
                };

                HttpResponseMessage response = await _httpClient.SubmitNewComplaintAsync(newComplaint);

                if (!response.IsSuccessStatusCode)
                {
                    tb_submit_connectionfailure.Visibility = Visibility.Hidden;
                    tb_submit_success.Visibility = Visibility.Hidden;
                    tb_submit_failure.Visibility = Visibility.Visible;
                }
                else
                {
                    tb_submit_connectionfailure.Visibility = Visibility.Hidden;
                    tb_submit_failure.Visibility = Visibility.Hidden;
                    tb_submit_success.Visibility = Visibility.Visible;
                }
            }
            catch (HttpRequestException ex)
            {
                if(ex.HttpRequestError == HttpRequestError.ConnectionError)
                {
                    tb_submit_success.Visibility = Visibility.Hidden;
                    tb_submit_failure.Visibility = Visibility.Hidden;
                    tb_submit_connectionfailure.Visibility = Visibility.Visible;
                }
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
                Tenant tenant = new()
                {
                    FirstName = tb_first_name.Text,
                    LastName = tb_last_name.Text
                };
                tenant = await _httpClient.FindTenantByNameAsync(tenant);

                if (tenant.Id == null)
                {
                    tb_issuer_connectionfailure.Visibility = Visibility.Hidden;
                    tb_issuer_found.Visibility = Visibility.Hidden;
                    tb_issuer_notfound.Visibility = Visibility.Visible;
                }
                else
                {
                    tb_issuer_connectionfailure.Visibility = Visibility.Hidden;
                    tb_issuer_notfound.Visibility = Visibility.Hidden;
                    tb_issuer_found.Visibility = Visibility.Visible;
                }
            }
            catch (HttpRequestException ex)
            {
                if(ex.HttpRequestError == HttpRequestError.ConnectionError)
                {
                    tb_issuer_notfound.Visibility = Visibility.Hidden;
                    tb_issuer_found.Visibility = Visibility.Hidden;
                    tb_issuer_connectionfailure.Visibility = Visibility.Visible;
                }
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
                await _httpClient.FindApartment(apartment);

                if (apartment.ApartmentNr == 0)
                {
                    tb_location_connectionfailure.Visibility = Visibility.Hidden;
                    tb_location_found.Visibility = Visibility.Hidden;
                    tb_location_notfound.Visibility = Visibility.Visible;
                }
                else
                {
                    tb_location_connectionfailure.Visibility = Visibility.Hidden;
                    tb_location_notfound.Visibility = Visibility.Hidden;
                    tb_location_found.Visibility = Visibility.Visible;
                }
            }
            catch (HttpRequestException ex)
            {
                if(ex.HttpRequestError == HttpRequestError.ConnectionError)
                {
                    tb_location_notfound.Visibility = Visibility.Hidden;
                    tb_location_found.Visibility = Visibility.Hidden;
                    tb_location_connectionfailure.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}