using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Membership
{
    public int Id { get; set; }

    public int Price { get; set; }

    public int? TeamId { get; set; }

    public int? SeatId { get; set; }

    public virtual ICollection<BookingMembership> BookingMemberships { get; set; } = new List<BookingMembership>();

    public virtual Seat? Seat { get; set; }

    public virtual Team? Team { get; set; }
}
