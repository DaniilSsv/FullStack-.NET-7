using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Seat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Row { get; set; }

    public int? SectionId { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual Section? Section { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
