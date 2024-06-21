using System.Windows;
using System.Windows.Controls;
using UMIASWPF.View.Authorization;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            DataContext = new ProfilePageVIewModel();
        }
    }
}
