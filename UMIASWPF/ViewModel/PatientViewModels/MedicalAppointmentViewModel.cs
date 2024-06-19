using BingingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using System.Collections.ObjectModel; // Добавьте эту директиву для ObservableCollection
using System.ComponentModel; // Добавьте эту директиву, если вы используете INotifyPropertyChanged
using UMIASWPF.Utilities;
using System.Windows.Documents;
using UMIASWPF.View.User.Pages;
using System.IO;
using UMIASWPF.View.User.UserEl;
using UMIASWPF.View.User;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class MedicalAppointmentViewModel : ApiHelper
    { 

        #region Region

        private string _appointmentName;

        public string AppointmentName
        {
            get => _appointmentName;
            set => SetField(ref _appointmentName, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetField(ref _address, value);
        }

        private string _doctor;

        public string Doctor
        {
            get => _doctor;
            set => SetField(ref _doctor, value);
        }

        private string _date;

        public string Date
        {
            get => _date;
            set => SetField(ref _date, value);
        }

        public FlowDocument RTB { get; set; }

        private ObservableCollection<MedicalAppointmentElement> _elements = new();

        public ObservableCollection<MedicalAppointmentElement> Elements
        {
            get => _elements;
            set => SetField(ref _elements, value);
        }

        private long _oms;

        private int _id;

        public MedicalAppointmentViewModel()
        {
            var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            RTB = new();
            LoadCards();
        }
        #endregion
        private void LoadCards()
        {
            var appointments = Get<List<Appointment>>("Appointments")!.Where(item => item.Oms == _oms).OrderBy(item => item.AppointmentDate).ToList();
            foreach (var appointment in appointments)
            {
                var researchDocument =
                    ApiHelper.Get<ResearchDocument>("AppointmentDocuments", (int)appointment.IdAppointment!);
                if (researchDocument != null)
                {
                    var doctor = ApiHelper.Get<DoctorModel>("Doctors", (int)appointment.DoctorId!);
                    var card = new MedicalAppointmentElement(researchDocument.DocumentName,
                        $"{doctor!.Surname} {doctor.FirstName.Substring(0, 1)}. {doctor.Patronymic.Substring(0, 1)}.",
                        appointment.AppointmentDate.ToString("dd MMMM yyyy"), doctor.WorkAddress, (int)doctor.IdDoctor,
                        (int)appointment.IdAppointment);
                    card.Click += (sender, args) => LoadInfo(sender, args);
                    Elements.Add(card);
                }
            }
        }

        private void LoadInfo(object sender, EventArgs args)
        {
            var card = sender as MedicalAppointmentElement;
            _id = card.IdAppointment;
            AppointmentName = card.NameAppointment;
            Doctor = card.NameDoctor;
            Address = card.Address;
            Date = card.Day;
            var document = Get<ResearchDocument>("AppointmentDocuments", card.IdAppointment);
            File.WriteAllText("buffer.rtf", document.Rtf);
            var range = new TextRange(RTB.ContentStart, RTB.ContentEnd);
            var fs = new FileStream("buffer.rtf", FileMode.Open);
            range.Load(fs, DataFormats.Rtf);
            fs.Close();
            File.Delete("buffer.rtf");
        }
    }
}