using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedBookingTicketDAO : BookingTicketDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedBookingTicketDAO(VoetbalDbContext voetbalDbContext) : base(voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<BookingTicket> FindBooking(int ticketId)
        {
            try
            {
                var bookingTicket = await _db.BookingTickets
                  .Where(bt => bt.TicketId == ticketId)
                  .FirstOrDefaultAsync();

                if (bookingTicket == null)
                {
                    throw new Exception("BookingTicket not found with the given ticket ID.");
                }

                return bookingTicket;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
