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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Documents;
using UMIASWPF.View.User.Pages;
using System.IO;
using UMIASWPF.View.User.UserEl;
using UMIASWPF.View.User;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    internal class MedicalAnaliysesViewModel : ApiHelper
    {
        #region Region

        private string _nameAnalys;

        public string NameAnalys
        {
            get => _nameAnalys;
            set => SetField(ref _nameAnalys, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetField(ref _address, value);
        }



        private string _day;

        public string Day
        {
            get => _day;
            set => SetField(ref _day, value);
        }

        public FlowDocument RTB { get; set; }

        private ObservableCollection<AnalysElement> _elements = new();

        public ObservableCollection<AnalysElement> Elements
        {
            get => _elements;
            set => SetField(ref _elements, value);
        }

        private long _oms;

        private int _id;

        #endregion


        public MedicalAnaliysesViewModel()
        {
            var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            _oms = Settings.Default.Patient;
            RTB = new();
            LoadCustomElements();
            LoadCards();
        }

        private void LoadCustomElements()
        {
            var customElementsFromApi = Get<List<AnalysElement>>("CustomElements");
            if (customElementsFromApi != null)
            {
                foreach (var customElement in customElementsFromApi)
                {
                    Elements.Add(customElement);
                }
            }
        }


        private void LoadCards()
        {
            var appointments = Get<List<Appointment>>("Appointments")!.Where(item => item.Oms == _oms).OrderBy(item => item.AppointmentDate);
            foreach (var appointment in appointments)
            {
                var analysDocument = ApiHelper.Get<AnalysDocument>("AnalysDocuments", (int)appointment.IdAppointment!);
                if (analysDocument != null)
                {
                    var doctor = ApiHelper.Get<DoctorModel>("Doctors",  (int)appointment.DoctorId!);

                    var card = new AnalysElement(analysDocument.DocumentName, 
                     appointment.AppointmentDate.ToString("dd MMMM yyyy"), doctor.WorkAddress, (int)doctor.IdDoctor, (int)appointment.IdAppointment);

                    card.Click += (sender, args) => LoadInfo(sender, args);
                    Elements.Add(card);
                }
            }
        }
        private void LoadInfo(object sender, EventArgs args)
        {
            var card = sender as AnalysElement;
            _id = card.IdAppointment;
            NameAnalys = card.NameAnalys;
            Address = card.Address;
            Day = card.Day;
            var document = Get<ResearchDocument>("ResearchDocuments", card.IdAppointment);
            File.WriteAllText("buffer.rtf", document.Rtf);
            var range = new TextRange(RTB.ContentStart, RTB.ContentEnd);
            var fs = new FileStream("buffer.rtf", FileMode.Open);
            range.Load(fs, DataFormats.Rtf);
            fs.Close();
            File.Delete("buffer.rtf");
        }
    }
}


   