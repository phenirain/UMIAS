using System.Windows;
using UMIASApp.View.Page;

namespace UMIASApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Start_window.Content = new Doctor_Autorize();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ToRollButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized; // Развернуть окно на весь экран
        }

    }
}

