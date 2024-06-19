using Newtonsoft.Json;
using System.Windows;
using UMIASWPF.Model;
using UMIASWPF.Properties;
using UMIASWPF.Utilities;
using UMIASWPF.View.User;
using UMIASWPF.View.User.Pages;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class PatientViewModel: ApiHelper
    {

        public List<SavingPatient> Patients { get; set; }
        public SavingPatient CurrentPatient { get; set; }

        public PatientViewModel()
        {
            Patients = JsonConvert.DeserializeObject<List<SavingPatient>>(Settings.Default.Patients);
            CurrentPatient = Patients.First(x => x.Oms == Settings.Default.Patient);
        }

        public void SelectionPatient()
        {
            Settings.Default.Patient = CurrentPatient.Oms;
            Settings.Default.Save();
            var window = Application.Current.Windows.OfType<PatientWindow>().First()!;
            window.Frame.Content = new ProfilePage();
        }

    }
}
