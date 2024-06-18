using System.Windows;
using System.Windows.Controls;
using UMIASWPF.Model;
using UMIASWPF.Utilities;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для AllPosts.xaml
    /// </summary>
    public partial class AllPostsElement : UserControl
    {
        public string ActiveMonth { get; set; }
        public List<AppointmentElement>? MonthAppointments {  get; set; }

        enum Monthes
        {
            Январь,
            Февраль,
            Март,
            Апрель, 
            Май, 
            Июнь, 
            Июль, 
            Август, 
            Сентябрь,
            Октябрь,
            Ноябрь,
            Декабрь
        }

        public AllPostsElement(int ActiveMonth, List<AppointmentElement>? appointments = null)
        {
            InitializeComponent();
            MonthAppointments = appointments;
            this.ActiveMonth = Enum.GetName(typeof(Monthes), ActiveMonth - 1).ToString();
            if (MonthAppointments == null) NoOne.Visibility = Visibility.Visible;
        }
    }
}
