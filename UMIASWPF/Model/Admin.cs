using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMIASWPF.Model
{
    public class Admin
    {
        public int? IdAdmin { get; set; }

        public string SurnameAdmin { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string EnterPassword { get; set; } = null!;
    }
}
