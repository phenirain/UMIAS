using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMIASWPF.Model
{
    public class SavingPatient
    {
        public string Nickname { get; set; }
        public long Oms { get; set; }

        public SavingPatient(string nickname, long oms)
        {
            Nickname = nickname;
            Oms = oms;
        }
    }
}
