namespace FullstackOpdracht.Domains.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public int Price { get; set; }

    public int? SeatId { get; set; }

    public int? MatchId { get; set; }

    public virtual ICollection<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();

    public virtual Match? Match { get; set; }

    public virtual Seat? Seat { get; set; }
}
