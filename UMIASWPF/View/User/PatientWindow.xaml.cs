using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UMIASWPF.View.User.Pages;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {

        public PatientWindow()
        {
            InitializeComponent();
            DataContext = new PatientViewModel();
            Frame.Content = new MainPage();
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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


        private void MedicalCard_Click(object sender, MouseButtonEventArgs e)
        {
            Frame.Content = new MedicalAppointmentsCardPage();
        }

        private void TreeHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = sender as TreeView;
            switch(tree.SelectedItem)
            {
                case "Приёмы":

                    break;
                case "Анализы":

                    break;
                case "Исследования":

                    break;
                case "Записи и направления":
                    Frame.Content = new MainPage();
                    break;
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new MedicalAppointmentsCardPage();
        }
    }
}
