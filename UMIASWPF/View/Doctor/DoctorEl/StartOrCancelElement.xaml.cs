using System.Windows.Controls;
using UMIASWPF.Model;

namespace UMIASWPF.View.Doctor.DoctorEl
{
    /// <summary>
    /// Логика взаимодействия для StartOrCancel.xaml
    /// </summary>
    public partial class StartOrCancelElement : UserControl
    {
        public Appointment appointment {  get; set; }
        public PatientModel patient { get; set; }
        public string FIO { get; set; }
        public string Time { get; set; }

        public event EventHandler Start;
        public event EventHandler Cancel;

        public StartOrCancelElement(string fIO, Appointment appointment, PatientModel patient)
        {
            InitializeComponent();
            this.appointment = appointment;
            this.patient = patient;
            FIO = fIO;
            Time = appointment.AppointmentTime.ToString();
            DataContext = this;
        }

        private void Begin(object sender, System.Windows.RoutedEventArgs e)
        {
            Start.Invoke(this, e);
        }

        private void Cancellation(object sender, System.Windows.RoutedEventArgs e)
        {
            Cancel.Invoke(this, e);
        }
    }
}
