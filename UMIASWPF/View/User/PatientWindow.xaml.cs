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

<<<<<<< HEAD

        private void MedicalCard_Click(object sender, MouseButtonEventArgs e)
        {
            Frame.Content = new MedicalAppointmentsCardPage();
        }

=======
        private void MedicalCard_Click(object sender, MouseButtonEventArgs e)
        {

        }
>>>>>>> c2e212d6f1aba8972a21b3f0d756f207afd0da29
        private void TreeHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = sender as TreeView;
            TreeViewItem item = tree.SelectedItem as TreeViewItem;
            switch(item.Header)
            {
                case "Приёмы":
                    Frame.Content = new MedicalAppointmentsCardPage();
                    break;
                case "Анализы":
                    Frame.Content = new MedCardAnalysesPage();
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
            Frame.Content = new ProfilePage();
        }
    }
}
