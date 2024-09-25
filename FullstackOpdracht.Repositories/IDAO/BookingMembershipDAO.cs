using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class BookingMembershipDAO : IDAO<BookingMembership>
    {
        private readonly VoetbalDbContext _dbContext;
        public BookingMembershipDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(BookingMembership entity)
        {
            try
            {
                await _dbContext.BookingMemberships.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(BookingMembership entity)
        {
            _dbContext.BookingMemberships.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BookingMembership> FindById(int id)
        {
            try
            {
                return await _dbContext.BookingMemberships.Where(s => s.BookingId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BookingMembership>> GetAll()
        {
            try
            {
                return await _dbContext.BookingMemberships.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(BookingMembership entity)
        {
            _dbContext.BookingMemberships.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
