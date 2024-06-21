using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using Spire.Doc;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using UMIASWPF.View.Doctor.DoctorEl;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System.Numerics;
using Spire.Doc.Documents;
using Newtonsoft.Json.Linq;
using System.Windows.Media;
using System.Xml.Linq;

namespace UMIASWPF.ViewModel
{
    public class DoctorViewModel: ApiHelper
    {
        #region obj

        private DateOnly _CurrentDate;
        public DateOnly CurrentDate
        {
            get => _CurrentDate;
            set => SetField(ref _CurrentDate, value);
        }
        private string _Complaints;
        public string Complaints
        {
            get => _Complaints;
            set => SetField(ref _Complaints, value);
        }

        private string _Inspection;
        public string Inspection
        {
            get => _Inspection;
            set => SetField(ref _Inspection, value);
        }

        private string _Diagnosis;
        public string Diagnosis
        {
            get => _Diagnosis;
            set => SetField(ref _Diagnosis, value);
        }

        private string _AdditionalDiagnosis;
        public string AdditionalDiagnosis
        {
            get => _AdditionalDiagnosis; 
            set => SetField(ref _AdditionalDiagnosis, value);
        }

        private string _Recomendations;
        public string Recomendations
        {
            get => _Recomendations;
            set => SetField(ref _Recomendations, value);
        }

        private string _Analys;
        public string Analys
        {
            get => _Analys;
            set => SetField(ref _Analys, value);
        }

        private string _Research;
        public string Research
        {
            get => _Research;
            set => SetField(ref _Research, value);
        }

        private Speciality _Speciality;
        public Speciality Speciality
        {
            get => _Speciality;
            set => SetField(ref _Speciality, value);
        }

        private string _FIO;
        public string FIO
        {
            get => _FIO;
            set => SetField(ref _FIO, value);
        }
        private PatientModel _Patient;
        public PatientModel Patient
        {
            get => _Patient;
            set => SetField(ref _Patient, value);
        }
        private FlowDocument _AnalysDocument = new();
        public FlowDocument AnalysesRTB
        {
            get => _AnalysDocument;
            set => SetField(ref _AnalysDocument, value);
        }

        private FlowDocument _ResearchDocument = new();
        public FlowDocument ResearchRTB
        {
            get => _ResearchDocument;
            set => SetField(ref _ResearchDocument, value);
        }

        private bool _GetResearch;
        public bool GetResearch
        {
            get => _GetResearch;
            set => SetField(ref _GetResearch, value);
        }

        private bool _GetAnalys;
        public bool GetAnalys
        {
            get => _GetAnalys;
            set => SetField(ref _GetAnalys, value);
        }

        private int _doctor = Settings.Default.Doctor;
        private int _patient;
        private DateTime _datetime;
        private byte[]? _image;
        private Appointment? _currentAppointment = null;
        #endregion

        #region collections
        private ObservableCollection<RecordCompletedElement> _EndAppointments = new();
        public ObservableCollection<RecordCompletedElement> EndAppointments
        {
            get => _EndAppointments;
            set => SetField(ref _EndAppointments, value);
        }

        private ObservableCollection<BlackStartOrCancelElement> _TimedOutAppointments = new();
        public ObservableCollection<BlackStartOrCancelElement> TimedOutAppointments
        {
            get => _TimedOutAppointments;
            set => SetField(ref _TimedOutAppointments, value);
        }

        private ObservableCollection<StartOrCancelElement> _CurrentAppointments = new();
        public ObservableCollection<StartOrCancelElement> CurrentAppointments
        {
            get => _CurrentAppointments;
            set => SetField(ref _CurrentAppointments, value);
        }

        private ObservableCollection<DirectionElement> _CurrentDirections = new();
        public ObservableCollection<DirectionElement> CurrentDirections
        {
            get => _CurrentDirections;
            set => SetField(ref _CurrentDirections, value);
        }

        public List<Speciality> Specialities { get; set; }
        #endregion
        public DoctorViewModel()
        {
            CurrentDate = DateOnly.FromDateTime(DateTime.Now);
            _datetime = DateTime.Now;
            _ = getAppointments();
            _ = getSpecialities();
        }

        #region getters
        private async Task getAppointments()
        {
            CurrentAppointments = new();
            EndAppointments = new();
            TimedOutAppointments = new();
            List<Appointment> appointments = Get<List<Appointment>>("Appointments").Where(x => x.DoctorId == _doctor
                && x.AppointmentDate == CurrentDate).OrderBy(x => x.AppointmentTime).ToList();
            List<PatientModel> patients = Get<List<PatientModel>>("Patients");
            var _dateonly = DateOnly.FromDateTime(_datetime);
            var _time = CurrentDate > _dateonly ? new TimeOnly(0, 0) : CurrentDate < _dateonly ? 
                new TimeOnly(23, 59) : TimeOnly.FromDateTime(_datetime);
            foreach (Appointment appointment in appointments)
            {
                PatientModel patient = patients.Where(x => x.Oms == appointment.Oms).First();
                string FIO = $"{patient.Surname} {patient.FirstName} {patient.Patronymic}";
                if (appointment.StatusId == 4)
                {
                    var ended = new RecordCompletedElement(FIO, appointment.AppointmentTime);
                    EndAppointments.Add(ended);
                    continue;
                }
                if (appointment.AppointmentTime < _time)
                {
                    var timedOut = new BlackStartOrCancelElement(FIO, appointment.AppointmentTime);
                    TimedOutAppointments.Add(timedOut);
                } else if (appointment.AppointmentTime > _time)
                {
                    var current = new StartOrCancelElement(FIO, appointment, patient);
                    current.Start += (sender, args) => StartAppointment(sender, args);
                    current.Cancel += (sender, args) => CancelAppointment(sender, args);
                    CurrentAppointments.Add(current);
                }
            }
        }

        private async Task getSpecialities()
        {
            Specialities = Get<List<Speciality>>("Specialities");
            Speciality = Specialities[0];
        }

        private async Task getDocuments()
        {
            var doc = new Document();
            doc.LoadFromFile("../../../Documents/Document1.docx");
            doc.SaveToFile("analysesBuffer.rtf", FileFormat.Rtf);
            var range = new TextRange(AnalysesRTB.ContentStart, AnalysesRTB.ContentEnd);
            using (var fs = new FileStream("analysesBuffer.rtf", FileMode.Open) ) {
                range.Load(fs, DataFormats.Rtf);
            }
            File.Delete("analysesBuffer.rtf");
            doc.LoadFromFile("../../../Documents/Document2.docx");
            doc.SaveToFile("researchBuffer.rtf", FileFormat.Rtf);
            range = new TextRange(ResearchRTB.ContentStart, ResearchRTB.ContentEnd);
            using (var fs = new FileStream("researchBuffer.rtf", FileMode.Open))
            {
                range.Load(fs, DataFormats.Rtf);
            }
            File.Delete("researchBuffer.rtf");
        }

        #endregion


        #region triggers
        public void SelecteDate(object sender, SelectionChangedEventArgs args)
        {
            CurrentDate = DateOnly.FromDateTime((sender as DatePicker).SelectedDate.Value);
            _ = getAppointments();
        }


        public void AddingDirection()
        {
            var direction = new DirectionElement(Speciality);
            direction.Delete += (sender, args) => DeleteDirection(sender, args);
            CurrentDirections.Add(direction);
        }
        public void AddFiles()
        {
            var dialog = new CommonOpenFileDialog { Title = "Open in Image" };
            dialog.Filters.Add(new CommonFileDialogFilter("Image Files", "*.jpg;*.jpeg;*.png;*.bmp;"));
            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                var image = System.Drawing.Image.FromFile(dialog.FileName);
                var memoryStream = new MemoryStream();
                image.Save(memoryStream, image.RawFormat);
                _image = memoryStream.ToArray();
                memoryStream.Dispose();
            }
        }

        public async void EndAppointment()
        {
            if (!string.IsNullOrEmpty(Diagnosis))
            {
                await getCorrectFile();
                if (GetAnalys) await getAppointmentDocument();
                if (GetResearch) await getResearchDocument();
                await postDirections();

                _currentAppointment.StatusId = 4;
                var jsonAppointment = JsonConvert.SerializeObject(_currentAppointment);
                ApiHelper.Put(jsonAppointment, "Appointments", (long)_currentAppointment.IdAppointment);

                CurrentAppointments.Remove(CurrentAppointments.Where(x => 
                            x.appointment.IdAppointment == _currentAppointment.IdAppointment).First());
                var ended = new RecordCompletedElement(FIO, _currentAppointment.AppointmentTime);
                EndAppointments.Add(ended);
                await Clear();
            } 
            else
            {
                MessageBox.Show("Укажите хотя бы диагноз!");
            }
        }
        #endregion

        #region delegetes
        private void StartAppointment(object sender, EventArgs args)
        {
            if (_currentAppointment == null)
            {
                var element = sender as StartOrCancelElement;
                Appointment appointment = element.appointment;
                Patient = element.patient;
                FIO = $"{Patient.Surname} {Patient.FirstName} {Patient.FirstName}";
                _ = getDocuments();
                _currentAppointment = appointment;
            } else
            {
                MessageBox.Show("Завершите текущий прием!");
            }
        }

        private void CancelAppointment(object sender, EventArgs args)
        {
            var element = sender as StartOrCancelElement;
            Appointment appointment = element.appointment;
            bool result = ApiHelper.Delete("Appointments", (long)appointment.IdAppointment);
            if (result)
            {
                CurrentAppointments.Remove(element);
                var ended = new RecordCompletedElement(element.FIO, element.appointment.AppointmentTime);
                EndAppointments.Add(ended);
            }
            else
            {
                MessageBox.Show("Не удалось удалить запись");
            }
        }

        private void DeleteDirection(object sender, EventArgs args)
        {
            CurrentDirections.Remove(sender as DirectionElement);
        }
        #endregion

        #region util
        private async Task getCorrectFile()
        {
            var doc = new Document();
            var section = doc.AddSection();
            DoctorModel doctor = Get<DoctorModel>("Doctors", _doctor);
            Speciality speciality = Get<Speciality>("Specialities", (long)doctor.SpecialityId);

            var title = section.AddParagraph();
            title.AppendText(
                $"Дата: {CurrentDate}\nПолис ОМС: {Patient.Oms}\nМедицинское учреждение:" +
                $" ГБУЗ ДКЦ 1 ДЗМ\nСпециализация: {speciality.NameSpecialities}" +
                $"\nФИО: {doctor.Surname} {doctor.FirstName} {doctor.Patronymic}\n");

            var inspection = section.AddParagraph();
            inspection.AppendText($"Осмотр {speciality.NameSpecialities}а");
            inspection.Format.HorizontalAlignment = 
                Spire.Doc.Documents.HorizontalAlignment.Center;
            inspection.BreakCharacterFormat.Bold = true;

            var table = section.AddTable();
            var rowCount = 4;
            var colCount = 2;
            table.ResetCells(rowCount, colCount);

            table.Rows[0].Cells[0].AddParagraph().AppendText("Жалобы: ");
            if (!string.IsNullOrEmpty(Complaints))
                table.Rows[0].Cells[1].AddParagraph().AppendText($"предъявляет\n{Complaints}\n");
            else
                table.Rows[0].Cells[1].AddParagraph().AppendText("не предъявляет");

            table.Rows[1].Cells[0].AddParagraph().AppendText("Общий осмтр:");
            table.Rows[1].Cells[1].AddParagraph().AppendText($"{Inspection}");

            table.Rows[2].Cells[0].AddParagraph().AppendText("Основной диагноз:");
            table.Rows[2].Cells[1].AddParagraph().AppendText($"{Diagnosis}. {AdditionalDiagnosis}");

            table.Rows[3].Cells[0].AddParagraph().AppendText("Рекомендация:");
            table.Rows[3].Cells[1].AddParagraph().AppendText($"{Recomendations}");

            table.Rows[0].Cells[0].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[0].Cells[1].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[1].Cells[0].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[1].Cells[1].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[2].Cells[0].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[2].Cells[1].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[3].Cells[0].CellFormat.Borders.Top.BorderType = BorderStyle.Single;
            table.Rows[3].Cells[1].CellFormat.Borders.Top.BorderType = BorderStyle.Single;

            doc.SaveToFile("receptionBuffer.rtf", FileFormat.Rtf);
            var reception = File.ReadAllText("receptionBuffer.rtf");
            File.Delete("receptionBuffer.rtf");

            var appointmentDocument =
                new AppointmentDocument(_currentAppointment.IdAppointment,
                reception, $"Осмотр {speciality.NameSpecialities}");
            var jsonAppointmentDocument = JsonConvert.SerializeObject(appointmentDocument);
            ApiHelper.Post(jsonAppointmentDocument, "AppointmentDocuments");
        }

        private async Task getAppointmentDocument()
        {
            var range = new TextRange(AnalysesRTB.ContentStart, AnalysesRTB.ContentEnd);
            using (var fs = new FileStream("analysesBuffer.rtf", FileMode.Create))
            {
                range.Save(fs, DataFormats.Rtf);
            }
            var analyse = File.ReadAllText("analysesBuffer.rtf");
            var analysDocument = new AnalysDocument((int)_currentAppointment.IdAppointment, analyse, Analys);
            string json = JsonConvert.SerializeObject(analysDocument);
            ApiHelper.Post(json, "AnalysDocuments");
            File.Delete("analysesBuffer.rtf");
        }

        private async Task getResearchDocument()
        {
            var range = new TextRange(ResearchRTB.ContentStart, ResearchRTB.ContentEnd);
            using (var fs = new FileStream("researchBuffer.rtf", FileMode.Create))
            {
                range.Save(fs, DataFormats.Rtf);
            }
            var research = File.ReadAllText("researchBuffer.rtf");
            var researchDocument = new AnalysDocument((int)_currentAppointment.IdAppointment, research, Research);
            string json = JsonConvert.SerializeObject(researchDocument);
            ApiHelper.Post(json, "ResearchDocuments");
            File.Delete("researchBuffer.rtf");
        }

        private async Task postDirections()
        {
            foreach (DirectionElement directionElement in CurrentDirections)
            {
                Direction direction = new Direction(specialityId: (int)directionElement.speciality.IdSpeciality,
                    oms: Patient.Oms);
                string json = JsonConvert.SerializeObject(direction);
                ApiHelper.Post(json, "Directions");
            }
        }

        private async Task Clear()
        {
            FIO = "";
            Patient = new();
            Complaints = "";
            Inspection = "";
            Diagnosis = "";
            AdditionalDiagnosis = "";
            Recomendations = "";
            CurrentDirections = new();
            GetAnalys = false;
            GetResearch = false;
            Analys = "";
            Research = "";
            AnalysesRTB = new();
            ResearchRTB = new();
            _image = null;
            _currentAppointment = null;
        }

        #endregion
    }
}
