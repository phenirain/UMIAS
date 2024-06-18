﻿using System;
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

namespace UMIASWPF.View.User.Pages
{
    /// <summary>
    /// Логика взаимодействия для MedicalAppointmentsCardPage.xaml
    /// </summary>
    public partial class MedicalAppointmentsCardPage : Page
    {
        private MedicalAppointmentViewModel _viewModel; 

        public MedicalAppointmentsCardPage()
        {
            InitializeComponent();
            _viewModel = new MedicalAppointmentViewModel();
            DataContext = _viewModel;
            //RTB.Document = _viewModel.RTB;
        }
    }
}
