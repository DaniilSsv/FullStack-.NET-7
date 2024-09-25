using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Stadium
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string City { get; set; } = null!;

    public int? TotalSeats { get; set; }

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
