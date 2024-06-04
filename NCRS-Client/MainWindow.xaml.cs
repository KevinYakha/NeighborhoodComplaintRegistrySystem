using System.Windows;

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
        }

        private void Overview_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Overview();
        }

        private void New_Complaint_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new NewComplaint();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Search();
        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }
    }
}