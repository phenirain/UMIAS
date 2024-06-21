using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedCardAnalyses.xaml
    /// </summary>
    public partial class MedCardAnalysesPage : Page
    {
        private MedicalAnaliysesViewModel _viewModel;

        public MedCardAnalysesPage()
        {
            InitializeComponent();
            _viewModel = new MedicalAnaliysesViewModel();
            DataContext = _viewModel;
            RTB.Document = _viewModel.RTB;
        }
    }
}
