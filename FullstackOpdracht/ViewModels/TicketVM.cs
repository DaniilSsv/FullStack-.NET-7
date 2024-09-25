using FullstackOpdracht.Domains.Entities;

namespace FullstackOpdracht.ViewModels
{
    public class TicketVM
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int? SeatId { get; set; }

        public int? MatchId { get; set; }
    }
}
