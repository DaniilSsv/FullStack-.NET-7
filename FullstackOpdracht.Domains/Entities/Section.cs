using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Section
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? RingId { get; set; }

    public virtual Ring? Ring { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
