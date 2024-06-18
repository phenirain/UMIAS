using System.IO.Compression;
using System.Linq.Expressions;
using UMIASWPF.Model;
using UMIASWPF.Utilities;
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
            set => SetField(ref _CurrentFrom, value);
        }

        private string _CurrentTo;
        public string CurrentTo
        {
            get => _CurrentTo;
            set => SetField(ref _CurrentTo, value);
        }

        private string _ArchiveFrom;
        public string ArchiveFrom
        {
            get => _ArchiveFrom; 
            set => SetField(ref _ArchiveFrom, value);
        }

        private string _ArchiveTo;
        public string ArchiveTo
        {
            get => _ArchiveFrom;
            set => SetField(ref _ArchiveFrom, value);
        }

        #endregion
        #region collections
        private List<DoctorElement> _Doctors;
        public List<DoctorElement> Doctors
        {
            get => _Doctors;
            set => SetField(ref _Doctors, value);
        }

        private List<AllPostsElement> _CurrentAppointment;
        public List<AllPostsElement> CurrentAppointments
        {
            get => _CurrentAppointment;
            set => SetField(ref _CurrentAppointment, value);
        }

        private List<AllPostsElement> _ArchiveAppointments;
        public List<AllPostsElement> ArchiveAppointments
        {
            get => _ArchiveAppointments;
            set => SetField(ref _ArchiveAppointments, value);
        }

        #endregion

        public MainPageViewModel()
        {
            CurrentAppointments = new List<AllPostsElement>();
            ArchiveAppointments = new List<AllPostsElement>();
            Doctors = new List<DoctorElement>();
            getDoctors();
            getCurrentAppointments();
            getArchiveAppointments();
        }

        private void getDoctors()
        {
            List<Speciality>? specialities = Get<List<Speciality>>("Specialities");
            Doctors.AddRange(from speciality in specialities
                             select new DoctorElement(speciality.NumberImage, speciality.NameSpecialities));
        }

        private void getCurrentAppointments()
        {
            List<Appointment>? appointments = Get<List<Appointment>>("Appointments");
            DateOnly currentFrom;
            DateOnly currentTo;
            if (CurrentFrom == null || CurrentTo == null)
            {
                CurrentFrom = DateTime.Now.Date.ToString();
                CurrentTo = DateTime.Now.AddMonths(2).Date.ToString();
                currentFrom = DateOnly.FromDateTime(DateTime.Now);
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
            DateOnly archiveFrom;
            DateOnly archiveTo;
            if (ArchiveFrom == null || ArchiveTo == null)
            {
                ArchiveFrom = DateTime.Now.AddMonths(-4).Date.ToString();
                ArchiveTo = DateTime.Now.AddMonths(-1).Date.ToString();
                archiveFrom = DateOnly.FromDateTime(DateTime.Now.AddMonths(-4));
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

        private void MoveAppointment(object sender, EventArgs args)
        {

        }

        private void RepeatAppointment(object sender, EventArgs args)
        {
            if (sender is AppointmentElement appointment)
            {
            }
        }
    }
}
