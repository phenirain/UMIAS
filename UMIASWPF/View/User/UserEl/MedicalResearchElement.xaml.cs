using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для MedicalResearchElement.xaml
    /// </summary>
    public partial class MedicalResearchElement : UserControl
    {
        public string NameResearch { get; set; }
        public string NameDoctor { get; set; }
        public string Day { get; set; }
        public string Address { get; set; }

        public int IdDoctor;

        public int IdAppointment;

        public event EventHandler Click;

        public MedicalResearchElement(string nameResearch, string nameDoctor, string day, string address, int idDoctor, int idAppointment)
        {
            InitializeComponent();
            DataContext = this;
            NameDoctor = nameDoctor;
            NameResearch = nameResearch;
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
