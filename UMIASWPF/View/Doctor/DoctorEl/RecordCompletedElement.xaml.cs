using System.Windows.Controls;

namespace UMIASWPF.View.Doctor.DoctorEl
{
    /// <summary>
    /// Логика взаимодействия для RecordCompleted.xaml
    /// </summary>
    public partial class RecordCompletedElement : UserControl
    {
        public string FIO { get; set; }
        public string Time { get; set; }

        public RecordCompletedElement(string fIO, TimeOnly time)
        {
            InitializeComponent();
            FIO = fIO;
            Time = time.ToString();
            DataContext = this;
        }
    }
}
