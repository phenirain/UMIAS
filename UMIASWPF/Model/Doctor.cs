using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMIASWPF.Model
{
    public class Doctor
    {
        public int? IdDoctor { get; set; }

        public string Surname { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public int? SpecialityId { get; set; }

        public string EnterPassword { get; set; } = null!;

        public string WorkAddress { get; set; } = null!;
    }
}
