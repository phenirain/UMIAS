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

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для AppointmentElement.xaml
    /// </summary>
    public partial class AppointmentElement : UserControl
    {
        public string NameDoctor { get; set; }
        public string FIO { get; set; }
        public string Day { get; set; }
        public string Address { get; set; }

        public int IdDoctor;

        public int IdAppointment;

        public event EventHandler Move;
        public event EventHandler Delete;

        public AppointmentElement(string nameDoctor, string fio, string day, string address, int idDoctor, int idAppointment)
        {
            InitializeComponent();
            DataContext = this;
            NameDoctor = nameDoctor;
            FIO = fio;
            Day = day;
            Address = address;
            IdAppointment = idAppointment;
            IdDoctor = idDoctor;
        }

        private void MoveClick(object sender, RoutedEventArgs e)
        {
            Move(this, EventArgs.Empty);
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Delete(this, EventArgs.Empty);
        }
    }
}
