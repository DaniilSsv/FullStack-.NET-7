using FullstackOpdracht.Domains.Entities;

namespace FullstackOpdracht.ViewModels
{
    public class MatchVM
    {
        public int Id { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }

        // teams
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}
