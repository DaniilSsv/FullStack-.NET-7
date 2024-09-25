using AutoMapper;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Extensions;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.Services.IService;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FullstackOpdracht.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IService<Team> _teamService;
        private readonly IMapper _mapper;
        private readonly ExtendedSectionService _sectionService;
        private readonly RingService _ringService;
        private readonly ExtendedMembershipService _membershipService;

        public SubscriptionController(IMapper mapper, IService<Team> team, ExtendedSectionService sectionService,
            RingService ringService, ExtendedMembershipService membershipService)
        {
            _mapper = mapper;
            _teamService = team;
            _sectionService = sectionService;
            _ringService = ringService;
            _membershipService = membershipService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var teamList = await _teamService.GetAll();
                List<TeamVM> teamVMs = new List<TeamVM>();
                if (teamList != null)
                {
                    teamVMs = _mapper.Map<List<TeamVM>>(teamList);
                }
                return View(teamVMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in controlller");
                return View("Index", "Profliga");
            }
        }

        public async Task<IActionResult> CreateAbonnement(int? id)
        {
            Team team = await _teamService.FindById(Convert.ToInt32(id));

            // Retrieve the sections/ rings for the match
            IEnumerable<Section> sections = await _sectionService.GetSectionsByStadium(Convert.ToInt32(id));
            IEnumerable<Ring> rings = await _ringService.GetRingsByStadium(Convert.ToInt32(id));

            if (team != null && sections != null && rings != null)
            {
                CreateMembershipVM model = new CreateMembershipVM
                {
                    TeamId = id,
                    Name = team.Name,
                    Sections = sections.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList(), // Sections ophalen van de stadium based op match, home team
                    Rings = rings.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList()// Rings ophalen van de stadium based op match, home team
                };
                return View(model);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbonnement(CreateMembershipVM membershipVM, int? teamID)
        {
            if (membershipVM == null)
            {
                return NotFound();
            }

            Team? team = await _teamService.FindById(Convert.ToInt32(teamID));
            Ring? ring = await _ringService.FindById(Convert.ToInt32(membershipVM.Ring));
            Section? section = await _sectionService.FindById(Convert.ToInt32(membershipVM.Section));

            ShoppingCartVM shopping;
            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            bool hasTicketInShopping = shopping.Cart.Any(m => m.TeamId == teamID);
            IEnumerable<Membership> membershipsOfUser = await _membershipService.GetAllMembershipsByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool hasMembershipFromTeam = membershipsOfUser.Any(t => t.Team.Id == teamID);


            if (team != null && !hasMembershipFromTeam && !hasTicketInShopping)
            {

                CartVM item = new CartVM
                {
                    Naam = team.Name,
                    Aantal = 1,
                    Prijs = 300,
                    TeamId = teamID,
                    Section = section, // zullen mogelijk geven om dit aan te passen
                    Ring = ring, // zullen mogelijk geven om dit aan te passen
                    DateCreated = DateTime.Now, // datum van match voor controle, geen nut om datum bij te houden van creatie ticket
                };

                shopping?.Cart?.Add(item);
                HttpContext.Session.SetObject("ShoppingCart", shopping);
            }
            else
            {
                return RedirectToAction("DuplicateAbonnement");
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult DuplicateAbonnement()
        {
            return View("DuplicateAbonnement");
        }


    }
}