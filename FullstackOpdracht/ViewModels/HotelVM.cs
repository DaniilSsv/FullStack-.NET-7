using System.Collections.Generic;

namespace FullstackOpdracht.ViewModels
{
    public class HotelVM
    {
        public List<Hotel> Hotels { get; set; }
    }

    public class Hotel
    {
        public string Name { get; set; }
        public double OverallRating { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string URL { get; set; }
    }
}