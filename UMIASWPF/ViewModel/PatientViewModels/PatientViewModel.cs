using UMIASWPF.Properties;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class PatientViewModel
    {
        public long OMS;

        public PatientViewModel()
        {
            OMS = Settings.Default.Patient;
        }


    }
}
