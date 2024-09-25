using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Match
{
    public int Id { get; set; }

    public int? HomeTeamId { get; set; }

    public int? AwayTeamId { get; set; }

    public DateTime MatchDate { get; set; }

    public virtual Team? AwayTeam { get; set; }

    public virtual Team? HomeTeam { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
