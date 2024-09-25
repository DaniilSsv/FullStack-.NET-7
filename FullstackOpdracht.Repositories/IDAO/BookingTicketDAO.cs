using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class BookingTicketDAO : IDAO<BookingTicket>
    {
        private readonly VoetbalDbContext _dbContext;
        public BookingTicketDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(BookingTicket entity)
        {
            try
            {
                await _dbContext.BookingTickets.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(BookingTicket entity)
        {
            _dbContext.BookingTickets.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BookingTicket> FindById(int id)
        {
            try
            {
                return await _dbContext.BookingTickets.Where(s => s.BookingId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BookingTicket>> GetAll()
        {
            try
            {
                return await _dbContext.BookingTickets.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(BookingTicket entity)
        {
            _dbContext.BookingTickets.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}