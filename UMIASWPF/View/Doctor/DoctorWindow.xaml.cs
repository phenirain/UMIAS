using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UMIASWPF.View.Doctor
{
    /// <summary>
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void UnwrapButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SwitchTheme(object sender, RoutedEventArgs e)
        {
            if (App.Theme == "Dark")
            {
                Button btn = (Button)sender;
                btn.Style = FindResource("SunnyStyle") as Style;
                App.Theme = "Light";
            }
            else
                App.Theme = "Dark";
        }

    }
}
