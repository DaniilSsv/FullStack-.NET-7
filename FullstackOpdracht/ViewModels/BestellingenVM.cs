namespace FullstackOpdracht.ViewModels
{

    public class BestellingenVM
    {
        public List<BestellingVM>? Bestelling { get; set; }
    }


    public class BestellingVM
    {
        public int? TicketId { get; set; }
        public int? MembershipId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public DateTime MatchDate { get; set; }
        public string? SeatName { get; set; }
        public int? SeatRow { get; set; }
        public bool CanDeleteFree { get; set; }
        public string OrderType {  get; set; }
    }
}