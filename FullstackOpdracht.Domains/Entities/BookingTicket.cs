using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class BookingTicket
{
    public int BookingId { get; set; }

    public int TicketId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
