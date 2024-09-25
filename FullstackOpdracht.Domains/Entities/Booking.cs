namespace FullstackOpdracht.Domains.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public virtual ICollection<BookingMembership> BookingMemberships { get; set; } = new List<BookingMembership>();

    public virtual ICollection<BookingTicket> BookingTickets { get; set; } = new List<BookingTicket>();

    public virtual AspNetUser User { get; set; } = null!;
}
