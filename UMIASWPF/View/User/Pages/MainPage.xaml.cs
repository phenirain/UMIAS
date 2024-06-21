using System.Windows.Controls;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }

    }
}
