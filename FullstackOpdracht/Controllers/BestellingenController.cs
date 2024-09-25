using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Services;
using FullstackOpdracht.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FullstackOpdracht.Controllers
{
    public class BestellingenController : Controller
    {
        private readonly ExtendedTicketService _extendedTicketService;
        private readonly ExtendedMatchService _matchService;
        private readonly ExtendedSeatService _seatService;
        private readonly ExtendedBookingTicketService _bookingTicketService;
        private readonly ExtendedMembershipService _MembershipService;


        public BestellingenController(ExtendedTicketService extendedTicketService, VoetbalDbContext context,
            ExtendedMatchService extendedMatchService, ExtendedSeatService seatService,
            ExtendedBookingTicketService bookingTicketService, ExtendedMembershipService extendedMembershipService)
        {
            _extendedTicketService = extendedTicketService;
            _matchService = extendedMatchService;
            _seatService = seatService;
            _bookingTicketService = bookingTicketService;
            _MembershipService = extendedMembershipService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var ticketList = await _extendedTicketService.GetAllTicketsByUserIdAsync(userId);
                var membershipList = await _MembershipService.GetAllMembershipsByUserIdAsync(userId);

                BestellingenVM bestellingen = new BestellingenVM();
                bestellingen.Bestelling = new List<BestellingVM>();

                if (membershipList != null)
                {
                    foreach (var membership in membershipList)
                    {
                       
                        Seat? seat = await _seatService.FindById(Convert.ToInt32(membership.SeatId));
                        BestellingVM item = new BestellingVM
                        {
                            MembershipId = membership.Id,
                            Name = membership.Team.Name,
                            Price = membership.Price,
                            MatchDate = DateTime.UtcNow.Date,
                            SeatName = seat.Name,
                            SeatRow = seat.Row,
                            OrderType = "Membership"
                        };
                        bestellingen.Bestelling.Add(item);
                    }
                }
                if (ticketList != null)
                {
                    // loop
                    foreach (var tickets in ticketList)
                    {
                       
                        Match? match = await _matchService.FindById(Convert.ToInt32(tickets.MatchId));
                        Seat? seat = await _seatService.FindById(Convert.ToInt32(tickets.SeatId));
                        if(match != null)
                        {
                            string matchName = match.HomeTeam.Name + " vs " + match.AwayTeam.Name;

                        BestellingVM item = new BestellingVM
                        {
                            TicketId = tickets.Id,
                            Name = matchName,
                            Price = tickets.Price,
                            MatchDate = match.MatchDate,
                            SeatName = seat.Name,
                            SeatRow = seat.Row,
                            OrderType = "Ticket",
                            CanDeleteFree = match.MatchDate.Subtract(DateTime.Now).TotalDays >= 7
                        };

                            bestellingen.Bestelling.Add(item);
                        }
                    }
                }

                return View(bestellingen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> PreDelete(int? ticketId)
        {
            if (ticketId == null)
            {
                return NotFound();
            }

            var ticket = await _extendedTicketService.FindById(Convert.ToInt32(ticketId));
            if (ticket == null)
            {
                return NotFound();
            }

            var match = await _matchService.FindById(Convert.ToInt32(ticket.MatchId));
            var seat = await _seatService.FindById(Convert.ToInt32(ticket.SeatId));

            string matchName = match.HomeTeam.Name + " vs " + match.AwayTeam.Name;

            BestellingVM item = new BestellingVM
            {
                TicketId = ticket.Id,
                Name = matchName,
                Price = ticket.Price,
                MatchDate = match.MatchDate,
                SeatName = seat.Name,
                SeatRow = seat.Row,
                CanDeleteFree = match.MatchDate.Subtract(DateTime.Now).TotalDays >= 7
            };

            return View(item);
        }


        public async Task<IActionResult> Delete(int? ticketId)
        {
            if (ticketId != null)
            {
                var bookingId = await _bookingTicketService.FindBooking(Convert.ToInt32(ticketId));

                if (bookingId != null)
                {
                    await _bookingTicketService.Delete(bookingId);
                   // await _context.SaveChangesAsync();

                    var ticket = await _extendedTicketService.FindById(Convert.ToInt32(ticketId));
                    await _extendedTicketService.Delete(ticket);
                    //await _context.SaveChangesAsync();
                };
            }
            return RedirectToAction("Index", "Bestellingen");
        }
    }
}
