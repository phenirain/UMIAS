using System.Windows.Controls;

namespace UMIASWPF.View.Doctor.DoctorEl
{
    /// <summary>
    /// Логика взаимодействия для BlackStartOrCancel.xaml
    /// </summary>
    public partial class BlackStartOrCancelElement : UserControl
    {
        public string FIO {  get; set; }
        public string Time { get; set; }


        public BlackStartOrCancelElement(string fIO, TimeOnly time)
        {
            InitializeComponent();
            FIO = fIO;
            Time = time.ToString();
            DataContext = this;
        }

    }
}
