using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMIASWPF.Model
{
    public class ResearchDocument
    {
        public ResearchDocument(int? idAppointment, string rtf, string documentName, byte[]? attachment = null)
        {
            IdAppointment = idAppointment;
            Rtf = rtf;
            DocumentName = documentName;
            Attachment = attachment;
        }

        public ResearchDocument()
        {

        }
        public int? IdAppointment { get; set; }

        public string Rtf { get; set; } = null!;

        public byte[]? Attachment { get; set; }

        public string DocumentName { get; set; } = null!;





    }
}
