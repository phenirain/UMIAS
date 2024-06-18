using System;
using System.Collections.Generic;

namespace UMAISAPI.Models;

public partial class ResearchDocument
{
    public int IdAppointment { get; set; }

    public string Rtf { get; set; } = null!;

    public virtual Appointment IdAppointmentNavigation { get; set; } = null!;
}
