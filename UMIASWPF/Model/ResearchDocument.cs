﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMIASWPF.Model
{
    public class ResearchDocument
    {
        public int? IdAppointment { get; set; }

        public string Rtf { get; set; } = null!;

        public byte[]? Attachment { get; set; }


    }
}
