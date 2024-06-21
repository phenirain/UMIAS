using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using UMIASWPF.View.User.UserEl;
using UMIASWPF.View.User;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Input;
using Wpf.Ui.Input;
using BingingLibrary;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    class MedicalResearchViewModel : ApiHelper
    {
        #region Region

        private string _nameResearch;

        public string NameResearch
        {
            get => _nameResearch;
            set => SetField(ref _nameResearch, value);
        }

        private string _address;

        public string Address
        {
            get => _address;
            set => SetField(ref _address, value);
        }

        private string _nameDoctor;

        public string NameDoctor
        {
            get => _nameDoctor;
            set => SetField(ref _nameDoctor, value);
        }

        private string _day;

        public string Day
        {
            get => _day;
            set => SetField(ref _day, value);
        }

        public FlowDocument RTB { get; set; }

        private ObservableCollection<MedicalResearchElement> _elements = new();

        public ObservableCollection<MedicalResearchElement> Elements
        {
            get => _elements;
            set => SetField(ref _elements, value);
        }

        private long _oms;

        private int _id;

        #endregion

        public BindableCommand DownloadCommand { get; private set; }
        public MedicalResearchViewModel()
        {
            var window = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault();
            _oms = Settings.Default.Patient;
            DownloadCommand = new BindableCommand(_=>Download());
            RTB = new();
            LoadCustomElements();
            LoadCards();
        }

        private void LoadCustomElements()
        {
            var customElementsFromApi = Get<List<MedicalResearchElement>>("CustomElements");
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
            var appointments = Get<List<Appointment>>("Appointments")!.Where(item => item.Oms == _oms).OrderBy(item => item.AppointmentDate).ToList();
            foreach (var appointment in appointments)
            {
                var researchDocument = ApiHelper.Get<ResearchDocument>("ResearchDocuments", (int)appointment.IdAppointment!);
                if (researchDocument != null)
                {
                    var doctor = ApiHelper.Get<DoctorModel>("Doctors", (int)appointment.DoctorId!);
                    var card = new MedicalResearchElement(researchDocument.DocumentName,
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
            var card = sender as MedicalResearchElement;
            _id = card.IdAppointment;
            NameResearch = card.NameResearch;
            NameDoctor = card.NameDoctor;
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

        private string saveFilePath = "";

        public void Download()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
            saveFileDialog.Title = "Выбери куда скачать файл";

            if (saveFileDialog.ShowDialog() == true)
            {
                saveFilePath = saveFileDialog.FileName;
                SaveRtfFile(saveFilePath);
            }
        }

        void SaveRtfFile(string _fileName)
        {
            TextRange range = new TextRange(RTB.ContentStart, RTB.ContentEnd);
            FileStream fst = new FileStream(_fileName, FileMode.Create);
            range.Save(fst, DataFormats.Rtf);
            fst.Close();
        }

    }
}
