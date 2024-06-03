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

        private void bt_submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Complaint newComplaint = new()
                {
                    Issuer = new() { FirstName = tb_first_name.Text, LastName = tb_last_name.Text },
                    ComplaintLocation = new() { ApartmentNr = int.Parse(tb_apartment_nr.Text) },
                    Description = tb_description.Text,
                    Category = (Complaint.ComplaintCategory)int.Parse(cb_category.Text)
                };

                HttpResponseMessage response;
                Task.Run(() =>
                {
                    Dispatcher.InvokeAsync(async () =>
                    {
                        response = await _httpClient.SubmitNewComplaintAsync(newComplaint);
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bt_find_issuer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tenant tenant = new()
                {
                    FirstName = tb_first_name.Text,
                    LastName = tb_last_name.Text
                };
                Task.Run(() =>
                {
                    Dispatcher.InvokeAsync(async () =>
                    {
                        await _httpClient.FindTenantByNameAsync(tenant);
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bt_find_location_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Apartment apartment = new()
                {
                    ApartmentNr = int.Parse(tb_apartment_nr.Text)
                };
                Task.Run(() =>
                {
                    Dispatcher.InvokeAsync(async () =>
                    {
                        await _httpClient.FindApartment(apartment);
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}