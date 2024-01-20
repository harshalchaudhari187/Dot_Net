using System;
using System.Collections.Generic;

namespace EmployeeMvcWithSecuirty.Models;

public partial class Team
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Abbreviation { get; set; }

    public string? Owner { get; set; }

    public int? MaxAge { get; set; }

    public int? BattingAvg { get; set; }

    public int? WicketsTaken { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
