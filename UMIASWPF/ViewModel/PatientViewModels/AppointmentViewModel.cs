using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMIASWPF.Model;
using UMIASWPF.Utilities;
using UMIASWPF.View.User.UserEl;

namespace UMIASWPF.ViewModel.PatientViewModels
{
    public class AppointmentViewModel: ApiHelper
    {
        #region collections
        public List<DoctorElement> ARI {  get; set; }
        public List<DoctorElement> Specialities { get; set; }
        public List<DoctorElement> Directions { get; set; }
        public List<DoctorElement> PurposeAppeals { get; set; } 

        #endregion


        public AppointmentViewModel()
        {
            ARI = new List<DoctorElement>();
            Specialities = new List<DoctorElement>();
            Directions = new List<DoctorElement>();
            PurposeAppeals = new List<DoctorElement>();
            _ = getARI();
            _ = getSpecialities();
            _ = getDirections();
            _ = getPurposes();
        }

        private async Task getARI()
        {
            var ari = new DoctorElement("1", "Дежурный врач по ОРВИ");
            ARI.Add(ari);
            var covid = new DoctorElement("covid", "Вакцинация от COVID-19");
            ARI.Add(covid);
        }

        private async Task getSpecialities()
        {
            var specialities = Get<List<Speciality>>("Specialities");
            Specialities.AddRange(from speciality in specialities
                                  select new DoctorElement(speciality.NumberImage.ToString(), speciality.NameSpecialities));
        }

        private async Task getDirections()
        {
            var directions = Get<List<Direction>>("Directions");
            var specialities = Get<List<Speciality>>("Specialities");
            foreach (var item in directions!)
            {
                var specialtyDoctor =
                    new DoctorElement(specialities![(int)(item.SpecialityId - 1)!].NumberImage.ToString(),
                        specialities[(int)(item.SpecialityId - 1)!].NameSpecialities);
                Directions.Add(specialtyDoctor);
            }
        }

        private async Task getPurposes()
        {
            var grip = new DoctorElement("vaccine", "Вакцинация от гриппа");
            PurposeAppeals.Add(grip);
            var acuteDisease = new DoctorElement("emergency", "Острое заболевание");
            PurposeAppeals.Add(acuteDisease);
            var orvi = new DoctorElement("1", "Дежурный врач ОРВИ");
            PurposeAppeals.Add(orvi);
            var inspection = new DoctorElement("3", "Осмотр по хронике");
            PurposeAppeals.Add(inspection);
            var document = new DoctorElement("document", "Оформить документы");
            PurposeAppeals.Add(document);
            var women = new DoctorElement("5", "Женская консультация");
            PurposeAppeals.Add(women);
            var profilactic = new DoctorElement("10", "Профилактика");
            PurposeAppeals.Add(profilactic);
            var dantist = new DoctorElement("6", "Запись к стоматологу");
            PurposeAppeals.Add(dantist);
        }

    }
}
