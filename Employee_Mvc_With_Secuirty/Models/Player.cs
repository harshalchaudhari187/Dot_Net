using System;
using System.Collections.Generic;

namespace EmployeeMvcWithSecuirty.Models;

public partial class Player
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public int? MinimumBattingAvg { get; set; }

    public int? MinimumWicketTaken { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }
}
