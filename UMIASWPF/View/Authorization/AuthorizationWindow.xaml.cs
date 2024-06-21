using System.Windows;
using System.Windows.Input;
using UMIASWPF.View.Authorization.Pages;
using UMIASWPF.View.Doctor;
using UMIASWPF.View.User;
using UMIASWPF.ViewModel;

namespace UMIASWPF.View.Authorization
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {

        AuthorizationViewModel _viewModel;
        public AuthorizationWindow()
        {
            InitializeComponent();
            _viewModel = new AuthorizationViewModel();
            DataContext = _viewModel;
            _viewModel.ToPatient += (_, _) => ToPatient();
            _viewModel.ToDoctor += (_, _) => ToDoctor();
            _viewModel.ToAdmin += (_, _) => ToAdmin();
            Start_window.Content = new PatientAuthorizationPage(_viewModel);
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

        private void ToAdmin()
        {
            Close();
        }

        private void ToDoctor()
        {
            DoctorWindow window = new DoctorWindow();
            window.Show();
            Close();
        }

        private void ToPatient()
        {
            PatientWindow window = new PatientWindow();
            window.Show();
            Close();
        }

    }
}
