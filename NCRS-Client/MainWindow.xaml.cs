using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            tb_name_search.Clear();
            dp_date_from.SelectedDate = null;
            dp_date_to.SelectedDate = null;
            MainContentFrame.Content = new Overview();
        }

        private void btn_NewComplaint_Click(object sender, RoutedEventArgs e)
        {
            new ManageComplaint().Show();
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // retrieve full list if there was no input anywhere
                if (tb_name_search.Text.Length == 0 && !dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                        MainContentFrame.Content = new Overview();
                        return;
                }
                // search only by name
                else if (tb_name_search.Text.Length > 0 && !dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                    string[] nameToSearch = tb_name_search.Text.Split(' ');

                    // do wildcard search if only one string was provided
                    if (nameToSearch.Length == 1)
                    {
                        MainContentFrame.Content = new Overview(nameToSearch[0]);
                        return;
                    }

                    // otherwise do a search for a partial fullname match
                    Tenant issuer = new()
                    {
                        FirstName = nameToSearch[0],
                        LastName = nameToSearch.Length > 1 ? nameToSearch[1] : ""
                    };

                    MainContentFrame.Content = new Overview(issuer);
                }
                // search by date to
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate == dp_date_to.SelectedDate)
                {
                    DateTime date = (DateTime)dp_date_to.SelectedDate;

                    MainContentFrame.Content = new Overview(date);
                }
                // search by date range
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate.HasValue && dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, (DateTime)dp_date_to.SelectedDate);

                    MainContentFrame.Content = new Overview(dateRange);
                }
                // search by date from date to today
                else if (tb_name_search.Text.Length == 0 && dp_date_from.SelectedDate.HasValue && !dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, DateTime.Today);

                    MainContentFrame.Content = new Overview(dateRange);
                }
                // search by date and name
                else if (tb_name_search.Text.Length > 0 && dp_date_from.SelectedDate == dp_date_to.SelectedDate)
                {
                    DateTime date = (DateTime)dp_date_to.SelectedDate;
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(date, issuer);
                }
                // search by date range and name
                else if (tb_name_search.Text.Length > 0 && dp_date_from.SelectedDate.HasValue && dp_date_to.SelectedDate.HasValue)
                {
                    Tuple<DateTime, DateTime> dateRange = new((DateTime)dp_date_from.SelectedDate, (DateTime)dp_date_to.SelectedDate);
                    string[] nameToSearch = tb_name_search.Text.Split(' ');
                    Tenant issuer = new() { FirstName = nameToSearch[0], LastName = nameToSearch[1] };

                    MainContentFrame.Content = new Overview(dateRange, issuer);
                }
                // search by date from date to today and name
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
            // make sure date from can't be higher than date to
            if (dp_date_to.SelectedDate < dp_date_from.SelectedDate || !dp_date_from.SelectedDate.HasValue)
            {
                dp_date_from.SelectedDate = dp_date_to.SelectedDate;
            }
        }

        private void tb_name_search_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btn_Search_Click(sender, null);
            }
        }
    }
}