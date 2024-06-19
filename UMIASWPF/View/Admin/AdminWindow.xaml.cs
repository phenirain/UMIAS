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
using System.Windows.Shapes;
using UMIASApp.View.Pages;
using UMIASWPF;

namespace UMIASApp.View
{
    /// <summary>
    /// Логика взаимодействия для Page_Admin.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            SelectionFrame.Content = new AdminPage();
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
            Button btn = (Button)sender;
            if (App.Theme == "Dark")
            {
                btn.Style = FindResource("MoonStyle") as Style;
                App.Theme = "Light";
            }
            else
            {
                btn.Style = FindResource("SunnyStyle") as Style;
                App.Theme = "Dark";
            }
        }
    }
}
