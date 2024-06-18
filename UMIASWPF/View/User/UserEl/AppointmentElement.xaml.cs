﻿using System.Windows.Controls;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для AppointmentElement.xaml
    /// </summary>
    public partial class AppointmentElement : UserControl
    {
        public int AppointmentId { get; set; }
        public string Speciality { get; set; }
        public string DoctorName { get; set; }
        public DateOnly AppointmentDate {  get; set; }
        public string AppointmentAddress { get; set; }

        public event EventHandler Delete;
        public event EventHandler Repeat;
        public event EventHandler Move;

        public AppointmentElement(int appointmentId, string speicality, string doctor, DateOnly date, string address, bool archive = false)
        {
            InitializeComponent();
            AppointmentId = appointmentId;
            Speciality = speicality;
            DoctorName = doctor;
            AppointmentDate = date;
            AppointmentAddress = address;
            DataContext = this;
            if (archive)
            {
                MoveOrRepeat.Click += RepeatClick;
                Cancel.Content = "Удалить";
            } else
            {
                MoveOrRepeat.Click += MoveClick;
                Cancel.Content = "Отменить";
            }
        }

        private void MoveClick(object sender, EventArgs e)
        {
            Move(this, EventArgs.Empty);
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            Delete(this, EventArgs.Empty);
        }

        private void RepeatClick(object sender, EventArgs e)
        {
            Repeat(this, EventArgs.Empty);
        }
    }
}
