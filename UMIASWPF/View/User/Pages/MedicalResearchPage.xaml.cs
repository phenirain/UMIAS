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
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User.Pages
{

    public partial class MedicalResearchPage : Page
    {
        private MedicalResearchViewModel _viewModel;
        public MedicalResearchPage()
        {
            InitializeComponent();

            _viewModel = new MedicalResearchViewModel();
            DataContext = _viewModel;
            RTB.Document = _viewModel.RTB;
        }
        
    }
}
