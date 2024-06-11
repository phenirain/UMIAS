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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UMIASApp.View.Page
{
    /// <summary>
    /// Логика взаимодействия для Doctor_Autorize.xaml
    /// </summary>
    public partial class Doctor_Autorize
    {
            public Doctor_Autorize()
            {
                InitializeComponent();

            }

            private void TextBox_GotFocus(object sender, RoutedEventArgs e)
            {
                TextBox textBox = sender as TextBox;
                if (textBox.Text == "Номер сотрудника")
                {
                    textBox.Text = "";
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }

            private void TextBox_LostFocus(object sender, RoutedEventArgs e)
            {
                TextBox textBox = sender as TextBox;
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Номер сотрудника";
                    textBox.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        private void TextBox_GotFocus1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Пароль")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBox_LostFocus1(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Пароль";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }


    }
    }
