using SerpApi;
using System.Collections;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using FullstackOpdracht.ViewModels;
using Hotel = FullstackOpdracht.ViewModels.Hotel;

namespace FullstackOpdracht.Controllers
{
    public class HotelController : Controller
    {
        private readonly ExtendedMatchService _matchService;
        private readonly IService<Stadium> _stadiumService;

        public HotelController(ExtendedMatchService matchService, IService<Stadium> stadiumService)
        {
            _matchService = matchService;
            _stadiumService = stadiumService;
        }

        public async Task<IActionResult> Index(HotelVM hotel)
        {
            return View(hotel);
        }

        //call
        public async Task<IActionResult> Hotel(int? id)
        {
            try
            {
                Match match = await _matchService.FindById(Convert.ToInt32(id));
                Stadium stadium = await _stadiumService.FindById(Convert.ToInt32(match.HomeTeam.StadiumId));

                string apiKey = "0df718e73eaae1d68a6cf50a4cfd90025e0c6d1ffb7511196a8a29465079df52";
                Hashtable ht = new Hashtable();
                ht.Add("engine", "google_hotels");
                ht.Add("q", stadium.City);
                ht.Add("check_in_date", DateTime.Now.ToString("yyyy-MM-dd"));
                ht.Add("check_out_date", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                ht.Add("gl", "be");
                ht.Add("hl", "nl");
                ht.Add("currency", "EUR");

                GoogleSearch search = new GoogleSearch(ht, apiKey);
                JObject data = search.GetJson();

                JArray properties = (JArray)data["properties"];
                var hotels = properties.Take(3).Select(hotel => new Hotel
                {
                    Name = hotel["name"]?.ToString() ?? "Unknown",
                    OverallRating = hotel["overall_rating"]?.ToObject<double>() ?? 0,
                    Price = hotel["rate_per_night"]?["lowest"]?.ToString() ?? "Unknown",
                    Image = hotel["images"]?.First?["thumbnail"]?.ToString() ?? "Unknown",
                    URL = hotel["link"]?.ToString() ?? "Unknown",
                }).ToList();

                var hotelViewModel = new HotelVM { Hotels = hotels };

                return View("Index", hotelViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving hotel information: {ex.Message}");
            }
        }
    }
}