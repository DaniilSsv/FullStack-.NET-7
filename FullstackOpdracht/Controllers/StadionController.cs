using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FullstackOpdracht.Controllers
{
    public class StadionController : Controller
    {
        private readonly IService<Stadium> _stadiumService;
        private readonly IMapper _mapper;
        private readonly ExtendedSeatService _seatService;

        public StadionController(IMapper mapper, IService<Stadium> stadium, ExtendedSeatService seatService)
        {
            _mapper = mapper;
            _stadiumService = stadium;
            _seatService = seatService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var listStadion = await _stadiumService.GetAll();
                var stadions = new List<StadiumVM>();
                foreach (var item in listStadion)
                {
                    var seats = await _seatService.GetSeatsByStadium(item.Id);
                    int seatCount = seats.Count;
                    item.TotalSeats = seatCount;


                    StadiumVM stadium = new StadiumVM
                    {
                        Id = item.Id,
                        Name = item.Name,
                        City = item.City,
                        Address = item.Address,
                        TotalSeats = seatCount
                    };
                    stadions.Add(stadium);
                    await _stadiumService.Update(item);
                }
                
                //var data = _mapper.Map<List<StadiumVM>>(listStadion);

                if (stadions != null)
                {
                    return View(stadions);
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Information(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var stadium = await _stadiumService.FindById(id);
                StadiumVM VM = _mapper.Map<StadiumVM>(stadium);
                return View(VM);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
