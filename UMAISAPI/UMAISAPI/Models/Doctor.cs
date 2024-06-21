using System;
using System.Collections.Generic;

namespace UMAISAPI.Models;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string Surname { get; set; } = null!;

    public string DoctorName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int IdSpeciality { get; set; }

    public string EnterPassword { get; set; } = null!;

    public string WorkAddress { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Speciality IdSpecialityNavigation { get; set; } = null!;
}
