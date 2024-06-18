﻿using System.Windows.Controls;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для DoctorElement.xaml
    /// </summary>
    public partial class DoctorElement : UserControl
    {
        public string Image {  get; set; }
        public string Doctor { get; set; }

        public DoctorElement(string Image, string Doctor)
        {
            InitializeComponent();
            DataContext = this;
            this.Image = $"../../../Images/{Image}.png";
            this.Doctor = Doctor;
        }
    }
}
