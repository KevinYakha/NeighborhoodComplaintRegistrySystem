using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

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

            dp_date_from.DisplayDateEnd = DateTime.Today;
            dp_date_to.DisplayDateEnd = DateTime.Today;
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new Overview();
        }

        private void btn_NewComplaint_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new NewComplaint();
        }

        private async void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_name_search.Text.Length > 0 && !dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(issuer);
                }
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate == dp_date_to.SelectedDate)
                {
                    DateTime date = (DateTime)dp_date_to.SelectedDate;

                    MainContentFrame.Content = new Overview(date);
                }
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate.HasValue && dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, (DateTime)dp_date_to.SelectedDate);

                    MainContentFrame.Content = new Overview(dateRange);
                }
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, DateTime.Today);

                    MainContentFrame.Content = new Overview(dateRange);
                }
                else if (tb_name_search.Text.Length > 0 && dp_date_from.SelectedDate == dp_date_to.SelectedDate)
                {
                    DateTime date = (DateTime)dp_date_to.SelectedDate;
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(date, issuer);
                }
                else if (tb_name_search.Text.Length > 0 && dp_date_from.SelectedDate.HasValue && dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, (DateTime)dp_date_to.SelectedDate);
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(dateRange, issuer);
                }
                else if (tb_name_search.Text.Length > 0 && dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, DateTime.Today);
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(dateRange, issuer);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void dp_date_to_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dp_date_to.SelectedDate < dp_date_from.SelectedDate || !dp_date_from.SelectedDate.HasValue)
            {
                dp_date_from.SelectedDate = dp_date_to.SelectedDate;
            }
        }
    }
}