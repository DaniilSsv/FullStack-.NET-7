using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class BookingMembership
{
    public int BookingId { get; set; }

    public int MembershipId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Membership Membership { get; set; } = null!;
}
