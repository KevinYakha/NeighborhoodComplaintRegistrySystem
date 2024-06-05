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
        public MainWindow()
        {
            InitializeComponent();

            dp_date_from.SelectedDate= DateTime.Today;
            dp_date_from.DisplayDateEnd = DateTime.Today;
            dp_date_to.SelectedDate= DateTime.Today;
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

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dp_date_to_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dp_date_to.SelectedDate < dp_date_from.SelectedDate)
            {
                dp_date_from.SelectedDate = dp_date_to.SelectedDate;
            }
        }
    }
}