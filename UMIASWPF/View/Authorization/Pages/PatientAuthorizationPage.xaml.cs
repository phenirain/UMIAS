using System.Windows;
using System.Windows.Controls;
using UMIASWPF.ViewModel;

namespace UMIASWPF.View.Authorization.Pages
{
    /// <summary>
    /// Логика взаимодействия для PatientAuthorizationPage.xaml
    /// </summary>
    public partial class PatientAuthorizationPage : Page
    {
        AuthorizationViewModel _viewModel;

        public PatientAuthorizationPage(AuthorizationViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void ToDoctorPage(object sender, EventArgs e)
        {
            if (Window.GetWindow(this) is AuthorizationWindow window)
            {
                window.Start_window.Content = new DoctorAuthorizationPage(_viewModel);
            }
        }

    }
}
