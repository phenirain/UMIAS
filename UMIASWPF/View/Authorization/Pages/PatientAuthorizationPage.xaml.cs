using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
