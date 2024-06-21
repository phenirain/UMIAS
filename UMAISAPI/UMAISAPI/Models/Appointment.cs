using System;
using System.Collections.Generic;

namespace UMAISAPI.Models;

public partial class Appointment
{
    public int? IdAppointment { get; set; }

    public long? Oms { get; set; }

    public int? IdDoctor { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public TimeOnly AppointmentTime { get; set; }

    public int? IdStatus { get; set; }

}
