using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using UMIASWPF.View.Authorization;
using UMIASWPF.View.User;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class ProfilePageVIewModel: ApiHelper
    {
        #region objects
        private PatientModel _Patient;
        public PatientModel Patient
        {
            get => _Patient;
            set => SetField(ref _Patient, value);
        }

        private int _SelectedTheme;
        public int SelectedTheme
        {
            get => _SelectedTheme;
            set => SetField(ref _SelectedTheme, value);
        }
        #endregion

        #region collections
        #endregion

        public ProfilePageVIewModel()
        {
            long _oms = Settings.Default.Patient;
            Patient = Get<List<PatientModel>>("Patients").FirstOrDefault(x => x.Oms == _oms)!;
            if (App.Theme == "Light")
                SelectedTheme = 0;
            else
                SelectedTheme = 1;
        }

        public void SelectionTheme()
        {
            if (SelectedTheme == 0)
                App.Theme = "Light";
            else
                App.Theme = "Dark";
        }
        public void Copy()
        {
            Patient.LivingAddress = Patient.AddressPatient;
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Patient);
            Put(json, "Patients", Patient.Oms);
        }

        public void Exit(object sender, RoutedEventArgs e)
        {
            List<SavingPatient> patients = JsonConvert.DeserializeObject<List<SavingPatient>>(Settings.Default.Patients);
            SavingPatient current = patients.First(x => x.Oms == Settings.Default.Patient);
            patients.Remove(current);
            string json = JsonConvert.SerializeObject(patients);
            Settings.Default.Patients = json;
            Settings.Default.Save();
            var main = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault()!;
            AuthorizationWindow window = new AuthorizationWindow();
            window.Show();
            main.Close();
        }

        public void NewAccount(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.Windows.OfType<PatientWindow>().FirstOrDefault()!;
            AuthorizationWindow window = new AuthorizationWindow();
            window.Show();
            main.Close();
        }

    }
}
