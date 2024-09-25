using System;
using System.Collections.Generic;

namespace FullstackOpdracht.Domains.Entities;

public partial class Ring
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StadiumId { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual Stadium? Stadium { get; set; }
}
