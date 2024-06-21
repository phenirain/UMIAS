using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UMIASWPF.View.Authorization;
using UMIASWPF.ViewModel;

namespace UMIASWPF.View.Doctor
{
    /// <summary>
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
            DoctorViewModel _viewModel = new DoctorViewModel();
            DataContext = _viewModel;
            Analyses.Document = _viewModel.AnalysesRTB;
            Researches.Document = _viewModel.ResearchRTB;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void UnwrapButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SwitchTheme(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (App.Theme == "Dark")
            {
                btn.Style = FindResource("MoonStyle") as Style;
                App.Theme = "Light";
            }
            else
            {
                btn.Style = FindResource("SunnyStyle") as Style;
                App.Theme = "Dark";
            }
        }

        private void Analys_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox box)
            {
                if ((bool)box.IsChecked)
                {
                    AnalysName.IsEnabled = true;
                    Analyses.Visibility = Visibility.Visible;
                }
                else
                {
                    AnalysName.IsEnabled = false;
                    Analyses.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Research_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox box)
            {
                if ((bool)box.IsChecked)
                {
                    ResearchName.IsEnabled = true;
                    Researches.Visibility = Visibility.Visible;
                }
                else
                {
                    ResearchName.IsEnabled = false;
                    Researches.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Leave(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow();
            window.Show();
            Close();
        }
    }
}
