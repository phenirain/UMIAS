using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UMIASWPF.Model;

namespace UMIASWPF.View.User.UserEl
{
    /// <summary>
    /// Логика взаимодействия для AllPosts.xaml
    /// </summary>
    public partial class AllPostsElement : UserControl
    {
        public string ActiveMonth;
        public ObservableCollection<Appointment> MonthAppointments;

        public AllPostsElement(string ActiveMonth, ObservableCollection<Appointment> appointments)
        {
            InitializeComponent();
            this.ActiveMonth = ActiveMonth;
            this.MonthAppointments = appointments;
            if (MonthAppointments.Count == 0) NoOne.Visibility = Visibility.Visible;
        }
    }
}
