using System.Windows;
using System.Windows.Controls;
using UMIASWPF.ViewModel;

namespace UMIASWPF.View.Authorization.Pages
{
    /// <summary>
    /// Логика взаимодействия для DoctorAuthorizationPage.xaml
    /// </summary>
    public partial class DoctorAuthorizationPage : Page
    {
        AuthorizationViewModel _viewModel;
        public DoctorAuthorizationPage(AuthorizationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void ToPatientPage(object sender, EventArgs e)
        {
            if (Window.GetWindow(this) is AuthorizationWindow window)
            {
                window.Start_window.Content = new PatientAuthorizationPage(_viewModel);
            }
        }
    }
}
