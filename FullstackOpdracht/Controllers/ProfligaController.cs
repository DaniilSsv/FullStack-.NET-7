using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FullstackOpdracht.Controllers
{
    public class ProfligaController : Controller
    {
        private readonly ExtendedMatchService _matchExService;
        private readonly IService<Team> _teamService;
        private readonly IMapper _mapper;
        private readonly ExtendedSectionService _sectionService;

        public ProfligaController(IMapper mapper, ExtendedMatchService matchEx, IService<Team> teamservice, ExtendedSectionService sectionService)
        {
            _mapper = mapper;
            _matchExService = matchEx;
            _teamService = teamservice;
            _sectionService = sectionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Voorstelling()
        {
            return View();
        }

        // toont kalender met alle matches
        public async Task<IActionResult> Kalender()
        {
            ViewBag.lstPloegen = new SelectList(await _teamService.GetAll(), "Id", "Name");

            try
            {
                var matchList = await _matchExService.GetAll();

                List<MatchVM> matchVMs = new List<MatchVM>();

                if (matchList != null)
                {
                    //sorteren
                    matchList = matchList.OrderByDescending(m => m.MatchDate).ToList();
                    matchVMs = _mapper.Map<List<MatchVM>>(matchList);
                }
                return View(matchVMs);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Matches(int teamId)
        {
            try
            {
                var matches = await _matchExService.GetMatchesForTeam(teamId);
                //sorteren
                matches = matches.OrderByDescending(m => m.MatchDate).ToList();

                var data = _mapper.Map<List<MatchVM>>(matches);
                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //zie alle matches van de komende maand, klik op bestellen en ga dan naar "createTicket"
        public async Task<IActionResult> Tickets()
        {
            try
            {
                var matchlst = await _matchExService.GetTicketMatches();
                List<MatchVM> matchVMs = new List<MatchVM>();
                if (matchlst != null)
                {
                    //sorteren
                    matchlst = matchlst.OrderBy(m => m.MatchDate).ToList();
                    matchVMs = _mapper.Map<List<MatchVM>>(matchlst);
                }
                return View(matchVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in controlller");
                return View("Index");
            }
        }

        

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult SetAppLanguage(string lang, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(lang)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1)
                    }
            );

            return LocalRedirect(returnUrl);
        }
    }
}