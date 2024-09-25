using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedTicketDAO : TicketDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedTicketDAO(VoetbalDbContext voetbalDbContext) : base(voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<Ticket> GetLast()
        {
            try
            {
                return await _db.Tickets.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsByUserIdAsync(string userId)
        {
            try
            {
                return await _db.Tickets
                    .Where(t => t.BookingTickets.Any(bt => bt.Booking.UserId == userId))
                    .Include(t => t.Match)
                    .ToListAsync();
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
