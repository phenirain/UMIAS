using System;
using System.Collections.Generic;

namespace UMAISAPI.Models;

public partial class Direction
{
    public string IdDirection { get; set; } = null!;

    public int? IdSpeciality { get; set; }

    public long? Oms { get; set; }

}
