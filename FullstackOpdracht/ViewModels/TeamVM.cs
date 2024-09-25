namespace FullstackOpdracht.ViewModels
{
    public class TeamVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? StadiumId { get; set; }

        public int? Price { get; set; }

        // You can decide whether to include the related entities in your ViewModel
        // Depending on your needs, you might want to include only the IDs, or not include them at all
        public ICollection<int> MatchAwayTeamIds { get; set; } = new List<int>();

        public ICollection<int> MatchHomeTeamIds { get; set; } = new List<int>();

        public ICollection<int> MembershipIds { get; set; } = new List<int>();
    }
}
