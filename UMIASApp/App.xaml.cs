using System;
using System.Windows;
using UMIASApp.Properties;

namespace UMIASApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static string _theme;

        public App()
        {
            InitializeComponent();
            Theme = Settings.Default.CurrentTheme;
        }

        public static string Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                var dick = new ResourceDictionary
                { Source = new Uri($"pack://application:,,,/Themes;component/{value}.xaml", UriKind.Absolute) };
                Current.Resources.MergedDictionaries.RemoveAt(0);
                Current.Resources.MergedDictionaries.Insert(0, dick);

                Settings.Default.CurrentTheme = value;
                Settings.Default.Save();
            }
        }


    }
}
