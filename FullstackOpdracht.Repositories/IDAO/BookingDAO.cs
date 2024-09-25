using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class BookingDAO : IDAO<Booking>
    {
        private readonly VoetbalDbContext _dbContext;
        public BookingDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(Booking entity)
        {
            try
            {
                await _dbContext.Bookings.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Booking entity)
        {
            _dbContext.Bookings.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Booking> FindById(int id)
        {
            try
            {
                return await _dbContext.Bookings.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                return await _dbContext.Bookings.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Booking entity)
        {
            _dbContext.Bookings.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
