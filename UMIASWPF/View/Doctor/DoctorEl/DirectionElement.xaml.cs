using System.Windows.Controls;
using UMIASWPF.Model;

namespace UMIASWPF.View.Doctor.DoctorEl
{
    /// <summary>
    /// Логика взаимодействия для Direction.xaml
    /// </summary>
    public partial class DirectionElement : UserControl
    {
        public Speciality speciality;
        public string Specialist { get; set; }

        public event EventHandler Delete;

        public DirectionElement(Speciality speciality)
        {
            InitializeComponent();
            this.speciality = speciality;
            Specialist = speciality.NameSpecialities;
            DataContext = this;
        }

        private void DeleteSelf(object sender, System.Windows.RoutedEventArgs e)
        {
           Delete?.Invoke(this, e);
        }
    }
}
