using System.Net;
using System.Windows;
using System.Windows.Controls;
using UMIASWPF.Model;
using UMIASWPF.View.User.UserEl;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для AnalyzElement.xaml
    /// </summary>
    public partial class AnalysElement : UserControl
    {
        public string NameAnalys { get; set; }
        public string Day { get; set; }
        public string Address { get; set; }

        public int IdDoctor;

        public int IdAppointment;

        public event EventHandler Click;

        public AnalysElement(string nameAnalys, string day, string address, int idDoctor, int idAppointment)
        {
            InitializeComponent();
            DataContext = this;
            NameAnalys = nameAnalys;
            Day = day;
            Address = address;
            IdAppointment = idAppointment;
            IdDoctor = idDoctor;
        }

        private void MClick(object sender, RoutedEventArgs e)
        {
            Click(this, EventArgs.Empty);
        }
    }
}


