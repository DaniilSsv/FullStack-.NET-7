namespace FullstackOpdracht.Domains.Entities;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? StadiumId { get; set; }

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual Stadium? Stadium { get; set; }
}
