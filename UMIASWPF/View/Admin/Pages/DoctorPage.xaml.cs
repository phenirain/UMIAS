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

namespace UMIASApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserDoctor.xaml
    /// </summary>
    public partial class DoctorPage : Page
    {
        AdminViewModel _viewModel;

        public DoctorPage(AdminViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
            InitializeComponent();
        }

    }
}
