using System.Windows.Controls;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User.Pages
{
    /// <summary>
    /// Логика взаимодействия для AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        public AppointmentPage()
        {
            InitializeComponent();
            DataContext = new AppointmentViewModel();
        }
    }
}
