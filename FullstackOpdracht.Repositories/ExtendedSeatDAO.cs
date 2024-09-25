using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedSeatDAO : SeatDAO
    {
        private readonly VoetbalDbContext _dbContext;

        public ExtendedSeatDAO(VoetbalDbContext context) : base(context)
        {
            _dbContext = context;
        }


        // vrije seats van een section
        public async Task<List<Seat>> GetFreeSeatsInSectionAsync(int id)
        {
            try
            {
                // Get all seats in the specified section
                var allSeatsInSection = await _dbContext.Seats
                    .Where(s => s.SectionId == id)
                    .ToListAsync();

                // Get all booked seats
                var bookedSeats = await _dbContext.BookingTickets
                    .Include(bt => bt.Ticket) // Include the Ticket entity in the query
                    .Select(bt => bt.Ticket.SeatId) // Select the SeatId from the Ticket entity
                    .ToListAsync();

                return allSeatsInSection
                .Where(seat => !bookedSeats.Contains(seat.Id))
                .ToList();
            } 
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<Seat>> GetSeatsByStadium(int id)
        {
            try
            {
                return await _dbContext.Seats
                    .Include(s => s.Section)
                    .Include(s => s.Section.Ring)
                    .Where(s => s.Section.Ring.StadiumId == id)
                    .ToListAsync();
            }
            catch ( Exception ex)
            {
                throw ex;
            }
        }
    }
}
