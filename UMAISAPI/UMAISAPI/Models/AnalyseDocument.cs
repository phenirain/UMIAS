using System;
using System.Collections.Generic;

namespace UMAISAPI.Models;

public partial class AnalyseDocument
{
    public int? IdAppointment { get; set; }

    public string Rtf { get; set; } = null!;

}
