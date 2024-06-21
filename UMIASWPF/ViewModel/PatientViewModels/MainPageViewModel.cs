using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using UMIASWPF.Model;
using UMIASWPF.Utilities;
using UMIASWPF.View.User;
using UMIASWPF.View.User.Pages;
using UMIASWPF.View.User.UserEl;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class MainPageViewModel : ApiHelper
    {
        #region obj
        private string _CurrentFrom;
        public string CurrentFrom
        {
            get => _CurrentFrom;
            set
            {
                SetField(ref _CurrentFrom, value);
            }
        }

        private string _CurrentTo;
        public string CurrentTo
        {
            get => _CurrentTo;
            set
            {
                SetField(ref _CurrentTo, value);
            }
        }

        private string _ArchiveFrom;
        public string ArchiveFrom
        {
            get => _ArchiveFrom; 
            set
            {
                SetField(ref _ArchiveFrom, value);
            }
        }

        private string _ArchiveTo;
        public string ArchiveTo
        {
            get => _ArchiveFrom;
            set
            {
                SetField(ref _ArchiveFrom, value);
            }
        }

        #endregion
        #region collections
        private ObservableCollection<DoctorElement> _Doctors;
        public ObservableCollection<DoctorElement> Doctors
        {
            get => _Doctors;
            set => SetField(ref _Doctors, value);
        }

        private ObservableCollection<AllPostsElement> _CurrentAppointment;
        public ObservableCollection<AllPostsElement> CurrentAppointments
        {
            get => _CurrentAppointment;
            set => SetField(ref _CurrentAppointment, value);
        }

        private ObservableCollection<AllPostsElement> _ArchiveAppointments;
        public ObservableCollection<AllPostsElement> ArchiveAppointments
        {
            get => _ArchiveAppointments;
            set => SetField(ref _ArchiveAppointments, value);
        }

        #endregion

        public MainPageViewModel()
        {
            CurrentAppointments = new ObservableCollection<AllPostsElement>();
            ArchiveAppointments = new ObservableCollection<AllPostsElement>();
            Doctors = new ObservableCollection<DoctorElement>();
            getDoctors();
            getCurrentAppointments();
            getArchiveAppointments();
        }

        private void getDoctors()
        {
            List<Speciality>? specialities = Get<List<Speciality>>("Specialities");
            foreach(var speciality in specialities)
            {
                Doctors.Add(new DoctorElement(speciality.NumberImage.ToString(), speciality.NameSpecialities));
            }
        }

        private void getCurrentAppointments()
        {
            List<Appointment>? appointments = Get<List<Appointment>>("Appointments");
            DateOnly currentFrom = DateOnly.FromDateTime(DateTime.Now);
            DateOnly currentTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(2));
            if (CurrentFrom == null && CurrentTo == null)
            {
                CurrentFrom = DateOnly.FromDateTime(DateTime.Now.Date).ToString();
                CurrentTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(2).Date).ToString();
                currentFrom = DateOnly.FromDateTime(DateTime.Now);
                currentTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(2));
            } 
            else if (CurrentFrom == null)
            {
                CurrentFrom = DateOnly.FromDateTime(DateTime.Now.Date).ToString();
                currentFrom = DateOnly.FromDateTime(DateTime.Now);
            } 
            else if (CurrentTo == null)
            {
                CurrentTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(2).Date).ToString();
                currentTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(2));
            }
            else
            {
                DateOnly.TryParse(CurrentTo, out currentTo);
                DateOnly.TryParse(CurrentFrom, out currentFrom);
            }
            List<KeyValuePair<int, List<Appointment>>>? current = appointments?.Where(x => x.AppointmentDate.Month >= currentFrom.Month
            && x.AppointmentDate.Month <= currentTo.Month).OrderBy(x => x.AppointmentDate.Month)
                .GroupBy(x => x.AppointmentDate.Month).ToDictionary(x => x.Key, x => x.ToList()).ToList();
            int currentCount = current?.Count < 3 ? 3 : current.Count;
            CurrentAppointments.Clear();
            for (int i = 0; i < currentCount; i++)
            {
                AllPostsElement post;
                if (current.Count - 1 >= i )
                {
                    KeyValuePair<int, List<Appointment>> pair = current[i];
                    post = new AllPostsElement(ActiveMonth: pair.Key, appointments: getAppointments(pair.Value));
                    CurrentAppointments.Add(post);
                }
            }
        }

        private void getArchiveAppointments()
        {
            List<Appointment>? appointments = Get<List<Appointment>>("Appointments");
            DateOnly archiveFrom = DateOnly.FromDateTime(DateTime.Now.AddMonths(-4));
            DateOnly archiveTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            if (ArchiveFrom == null && ArchiveTo == null)
            {
                ArchiveFrom = DateTime.Now.AddMonths(-4).Date.ToString();
                ArchiveTo = DateTime.Now.AddMonths(-1).Date.ToString();
            } 
            else if (ArchiveFrom == null)
            {
                ArchiveFrom = DateTime.Now.AddMonths(-4).Date.ToString();
                archiveFrom = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            }
            else if (ArchiveTo == null)
            {
                ArchiveTo = DateTime.Now.AddMonths(-1).Date.ToString();
                archiveTo = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
            }
            else
            {
                DateOnly.TryParse(ArchiveTo, out archiveTo);
                DateOnly.TryParse(ArchiveFrom, out archiveFrom);
            }
            List<KeyValuePair<int, List<Appointment>>>? archive = appointments?.Where(x => x.AppointmentDate > archiveFrom && x.AppointmentDate < archiveTo)
                .ToList()
                .GroupBy(x => x.AppointmentDate.Month).ToDictionary(x => x.Key, x => x.ToList()).ToList();
            int archiveCount = archive?.Count < 3 ? 3 : archive.Count;
            ArchiveAppointments.Clear();
            for (int i = 0; i < archiveCount;i++)
            {
                AllPostsElement post;
                if (archive.Count - 1 >= i)
                {
                    KeyValuePair<int, List<Appointment>> pair = archive[i];
                    post = new AllPostsElement(ActiveMonth: pair.Key, appointments: getAppointments(pair.Value, true));
                    ArchiveAppointments.Add(post);
                }
            }
        }

        private List<AppointmentElement> getAppointments(List<Appointment>? appointments, bool archive = false)
        {
            List<AppointmentElement> MonthAppointments = new List<AppointmentElement>();
            foreach (var appointment in appointments)
            {
                DoctorModel? doctor = ApiHelper.Get<DoctorModel>("Doctors", (int)appointment.DoctorId);
                Speciality? speciality = ApiHelper.Get<Speciality>("Specialities", (int)doctor.SpecialityId);
                AppointmentElement element = new AppointmentElement(appointmentId: (int)appointment.IdAppointment, 
                    speicality: speciality.NameSpecialities, doctor: doctor.FirstName + " " + doctor.Surname
                    + " " + doctor.Patronymic, date: appointment.AppointmentDate, address: doctor.WorkAddress,
                    archive: archive
                    );
                element.Delete += (sender, args) => DeleteAppoinment(sender, args);
                element.Repeat += (sender, args) => RepeatAppointment(sender, args);
                element.Move += (sender, args) => MoveAppointment(sender, args);
                MonthAppointments.Add(element);
            }
            return MonthAppointments;
        }

        private void DeleteAppoinment(object sender, EventArgs args)
        {
            if (sender is AppointmentElement appointment)
            {
                int id = appointment.AppointmentId;
                foreach (AllPostsElement item in CurrentAppointments)
                    if (item.MonthAppointments.Remove(appointment))
                        break;
                foreach (AllPostsElement item in ArchiveAppointments)
                    if (item.MonthAppointments.Remove(appointment))
                        break;
                Delete("AnalysDocuments", id);
                Delete("ResearchDocuments", id);
                Delete("AppointmentDocuments", id);
                Delete("Appointments", id);
            }
        }

        public void ToAppointmentPage()
        {
            var window = Application.Current.Windows.OfType<PatientWindow>().First()!;
            window.Frame.Content = new AppointmentPage();
        }

        private void MoveAppointment(object sender, EventArgs args)
        {
            Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault()!.Frame.Content = new SpecialistSelectionPage();
        }

        private void RepeatAppointment(object sender, EventArgs args)
        {
            Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault()!.Frame.Content = new SpecialistSelectionPage();
        }

        public void SelectedDateCurrentFrom(object? sender, SelectionChangedEventArgs e)
        {
            CurrentFrom = DateOnly.FromDateTime((DateTime)((sender as DatePicker)!).SelectedDate!).ToString();
            getCurrentAppointments();
        }

        public void SelectedDateCurrentTo(object? sender, SelectionChangedEventArgs e)
        {
            CurrentTo = DateOnly.FromDateTime((DateTime)((sender as DatePicker)!).SelectedDate!).ToString();
            getCurrentAppointments();
        }

        public void SelectedDateArchivesFrom(object? sender, SelectionChangedEventArgs e)
        {
            ArchiveFrom = DateOnly.FromDateTime((DateTime)((sender as DatePicker)!).SelectedDate!).ToString();
            getArchiveAppointments();
        }

        public void SelectedDateArchivesTo(object? sender, SelectionChangedEventArgs e)
        {
            ArchiveTo = DateOnly.FromDateTime((DateTime)((sender as DatePicker)!).SelectedDate!).ToString();
            getArchiveAppointments();
        }
    }
}
