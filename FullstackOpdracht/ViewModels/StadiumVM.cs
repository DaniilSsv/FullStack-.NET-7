using FullstackOpdracht.Domains.Entities;

namespace FullstackOpdracht.ViewModels
{
    public class StadiumVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? TotalSeats { get; set; }
        public ICollection<Ring>? Rings { get; set; }
        public ICollection<Team>? Teams { get; set; }
    }
}
