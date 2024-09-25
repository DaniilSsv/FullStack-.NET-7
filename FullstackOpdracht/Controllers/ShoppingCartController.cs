using AutoMapper;
using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Extensions;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using FullstackOpdracht.Util.Interfaces;
using FullstackOpdracht.Util.PDF.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections;
using SerpApi;
using FullstackOpdracht.Areas.Data;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Threading;


namespace FullstackOpdracht.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IEmailSend _emailSend;
        private ICreatePDF _createPDF;
        private readonly ExtendedTicketService _ticketService;
        private readonly ExtendedMembershipService _membershipService;
        private readonly ExtendedMatchService _matchService;
        private readonly ExtendedSectionService _sectionService;
        private readonly ExtendedSeatService _seatService;
        private readonly RingService _ringService;
        private readonly ExtendedBookingTicketService _bookingTicketService;
        private readonly ExtendedBookingService _bookingService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IService<Stadium> _stadiumService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly ExtendedUserService _userService;
        private readonly IService<BookingMembership> _bookingMembershipService;
        private readonly IService<Team> _teamService;
        private readonly IMapper _mapper;

        public ShoppingCartController(ExtendedSeatService seatService, ExtendedTicketService ticketService,
            ExtendedMembershipService membershipService, IMapper mapper,
            ExtendedMatchService matchService, ExtendedSectionService sectionService, RingService ringService,
            ExtendedBookingService bookingService, IEmailSend emailSend, IWebHostEnvironment hostingEnvironment,
            ICreatePDF createPDF, IService<Stadium> stadiumservice, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,
            ExtendedUserService userService, ExtendedBookingTicketService bookingTicketService, IService<BookingMembership> bookingMembership)
        {
            _ticketService = ticketService;
            _membershipService = membershipService;
            _mapper = mapper;
            _matchService = matchService;
            _sectionService = sectionService;
            _ringService = ringService;
            _mapper = mapper;
            _seatService = seatService;
            _bookingService = bookingService;
            _emailSend = emailSend;
            _hostingEnvironment = hostingEnvironment;
            _createPDF = createPDF;
            _stadiumService = stadiumservice;
            _userManager = userManager;
            _userService = userService;
            _bookingTicketService = bookingTicketService;
            _bookingMembershipService = bookingMembership;
        }

        [Authorize]
        public IActionResult Index()
        {
            ShoppingCartVM? cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            return View(cartList);
        }

        public async Task<IActionResult> CreateTicket(int? id)
        {
            // Retrieve the match with the given id
            Match match = await _matchService.FindById(Convert.ToInt32(id));

            // Retrieve the sections/ rings for the match
            IEnumerable<Section> sections = await _sectionService.GetSectionsByStadium(Convert.ToInt32(match.HomeTeam.StadiumId));
            IEnumerable<Ring> rings = await _ringService.GetRingsByStadium(Convert.ToInt32(match.HomeTeam.StadiumId));

            if (match != null && sections != null && rings != null)
            {
                CreateTicketVM model = new CreateTicketVM
                {
                    matchID = id,
                    Name = match.HomeTeam.Name + " vs " + match.AwayTeam.Name,
                    Sections = sections.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList(), // Sections ophalen van de stadium based op match, home team
                    Rings = rings.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList()// Rings ophalen van de stadium based op match, home team
                };
                return View(model);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(CreateTicketVM ticketVM, int? matchID)
        {
            if (ticketVM == null)
            {
                return NotFound();
            }

            Match? match = await _matchService.FindById(Convert.ToInt32(matchID));
            Ring? ring = await _ringService.FindById(Convert.ToInt32(ticketVM.Ring));
            Section? section = await _sectionService.FindById(Convert.ToInt32(ticketVM.Section));

            int price = GetPriceForMatch(ring, section);

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

            DateTime newMatchDate = match.MatchDate;
            
            bool hasMatchOnSameDay = shopping.Cart.Any(ticket => ticket.DateCreated.Day == newMatchDate.Day);
            IEnumerable<Ticket> ticketsOfUser = await _ticketService.GetAllTicketsByUserIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            bool hasTicketFromSameDay = ticketsOfUser.Any(t => t.Match.MatchDate.Day == newMatchDate.Day);



            if (match != null && !hasMatchOnSameDay && !hasTicketFromSameDay)
            {
                string matchName = match.HomeTeam.Name + " vs " + match.AwayTeam.Name;

                CartVM item = new CartVM
                {
                    Naam = matchName,
                    Aantal = 1,
                    Prijs = price,
                    MatchId = match.Id,
                    Section = section, // zullen mogelijk geven om dit aan te passen
                    Ring = ring, // zullen mogelijk geven om dit aan te passen
                    DateCreated = match.MatchDate, // datum van match voor controle, geen nut om datum bij te houden van creatie ticket
                };
                
                shopping?.Cart?.Add(item);
                HttpContext.Session.SetObject("ShoppingCart", shopping);
            } else
            {
                return RedirectToAction("DuplicateTicket");
            }
            
            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult DuplicateTicket()
        {
            return View("DuplicateTicket");
        }

        public async Task<IActionResult> GetSections(int ringId)
        {
            var sections = await _sectionService.GetSectionsByRing(ringId);
            return Json(sections.Select(s => new { id = s.Id, name = s.Name }));
        }


        public int GetPriceForMatch(Ring ring, Section section)
        {
            float basePrice = 30;
            float ringMultiplier = 1.0f;
            float sectionMultiplier = 1.0f;

           
            // Check the ring name and apply the appropriate multiplier
            if (ring.Name == "BovensteRing")
            {
                ringMultiplier = 1.5f;
            }
            else if (ring.Name == "OndersteRing")
            {
                ringMultiplier = 1.2f;
            }

            // Check the section name and apply the appropriate multiplier
            if (section.Name == "Onderste Ring Thuis" || section.Name == "Onderste Ring Bezoekers")
            {
                sectionMultiplier = 1.3f;
            }
            else if (section.Name == "Onderste Ring Oost" || section.Name == "Onderste Ring West")
            {
                sectionMultiplier = 1.0f;
            }
            // Check the section name and apply the appropriate multiplier
            if (section.Name == "Bovenste Ring Thuis" || section.Name == "Bovenste Ring Bezoekers")
            {
                sectionMultiplier = 1.3f;
            }
            else if (section.Name == "Bovenste Ring Oost" || section.Name == "Bovenste Ring West")
            {
                sectionMultiplier = 1.0f;
            }

            // Calculate the final price
            int finalPrice =  Convert.ToInt32(basePrice * ringMultiplier * sectionMultiplier);

            return finalPrice;
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Checkout(ShoppingCartVM shoppingCart)
        {
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

            var mailTickets = new List<Ticket>();
            var mailMatches = new List<Match>();
            var mailSections = new List<string>();

            var mailMemberships = new List<Membership>();
            var mailTeams = new List<string>();
            var membershipSections = new List<string>();
            // Loop through each item in the shopping cart
            for (int j = 0; j < shopping.Cart.Count; j++)
            {
                if (shopping.Cart[j] != null)
                {
                    // alle vrije seats ophalen
                    var freeSeats = await _seatService.GetFreeSeatsInSectionAsync(shopping.Cart[j].Section.Id);

                    if (freeSeats.Count > shopping.Cart[j].Aantal)
                    {
                        var booking = new Booking
                        {
                            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        };

                        await _bookingService.Add(booking);
                        var dbBooking = await _bookingService.GetLast();

                        //check ticket or subscription
                        if (shopping.Cart[j].MatchId != null)
                        {
                            // ticket aanmaken
                            for (int i = 0; i < shoppingCart.Cart[j].Aantal; i++)
                            {
                                var seat = freeSeats[0]; // first free seat
                                freeSeats.RemoveAt(0); // Remove the seat from list
    
                                var ticket = new Ticket
                                {
                                    Price = Convert.ToInt32(shopping.Cart[j].Prijs),
                                    SeatId = seat.Id,
                                    MatchId = shopping.Cart[j].MatchId,
                                };

                                await _ticketService.Add(ticket);
                                //ticket ophalen uit database zodat je ID hebt..
                                var dbTicket = await _ticketService.GetLast();

                                // ticket linken met de booking
                                var bookingTicket = new BookingTicket
                                {
                                    BookingId = dbBooking.Id,
                                    TicketId = dbTicket.Id,
                                };

                                // bookingTicket toevoegen aan db en db updaten
                                await _bookingTicketService.Add(bookingTicket);

                                // MAIL VERSTUREN
                                var match = await _matchService.FindById(Convert.ToInt32(shopping.Cart[j].MatchId));
                                mailTickets.Add(dbTicket);
                                mailMatches.Add(match);
                                mailSections.Add(shopping.Cart[j].Section.Name);
                            }
                        }else
                        {
                            // membership aanmaken
                            var seat = freeSeats[0]; // first free seat
                            freeSeats.RemoveAt(0); // Remove the seat from list

                            var membership = new Membership
                            {
                                Price = Convert.ToInt32(shopping.Cart[j].Prijs),
                                SeatId = seat.Id,
                                TeamId = shopping.Cart[j].TeamId
                            };
                            await _membershipService.Add(membership);
                            var dbMembership = await _membershipService.GetLast();

                            // ticket linken met de booking
                            var bookingMembership = new BookingMembership
                            {
                                BookingId = dbBooking.Id,
                                MembershipId = dbMembership.Id,
                            };
                            await _bookingMembershipService.Add(bookingMembership);
                            mailTeams.Add(shopping.Cart[j].Naam);
                            mailMemberships.Add(dbMembership);
                            membershipSections.Add(shopping.Cart[j].Section.Name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Geen seats Beschikbaar");
                        return View("NoSeat");
                    }
                } else
                {
                    Console.WriteLine("Geen items van cart meegegeven");
                }  
            }
            await SendTicketInMail(mailTickets, mailMatches, mailSections, mailMemberships, mailTeams, membershipSections);
            return View();
        }

        public async Task SendTicketInMail(List<Ticket> tickets, List<Match> matches, List<string> sections,
            List<Membership> memberships, List<string> teamNames, List<string> membershipsections) {
            
            string pdfFile = "Ticket" + DateTime.Now.Year;
            var pdfFileName = $"{pdfFile}_{Guid.NewGuid()}.pdf";
            var user = await _userService.FindById(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Het pad naar de map waarin het logo zich bevindt
            string logoPath =
                Path.Combine(_hostingEnvironment.WebRootPath, "images", "proleague.jpg");


            var fileStreamList = new List<MemoryStream>();

            for (int i = 0; i < tickets.Count; i++) {
                var pdfDocument = _createPDF.CreatePDFDocumentAsync(tickets[i], matches[i], logoPath, user.FullName, sections[i]);
                fileStreamList.Add(pdfDocument);
            }
            for (int i = 0; i < memberships.Count; i++)
            {
                var pdfDocument = _createPDF.CreatePDFDocumentMembership(memberships[i], membershipsections[i], teamNames[i], logoPath, user.FullName);
                fileStreamList.Add(pdfDocument);
            }

            await _emailSend.SendEmailAttachmentAsync(user.Email, "tickets", "in bijlage vindt u uw voetbalticket", fileStreamList, pdfFileName);
        }

        public IActionResult Delete(int? CartId)
        {
            ShoppingCartVM? shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }

            int i = 0;
            var removed = false;
            while (!removed)
            {
                if (shopping.Cart[i].CartId == CartId)
                {
                    shopping.Cart.Remove(shopping.Cart[i]);
                    removed = true;
                }
                i++;
            }
            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return View("Index", shopping);
        }
    }
}
