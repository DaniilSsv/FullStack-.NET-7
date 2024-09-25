using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class SeatDAO : IDAO<Seat>
    {
        private readonly VoetbalDbContext _dbContext;
        public SeatDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(Seat entity)
        {
            try
            {
                await _dbContext.Seats.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Seat entity)
        {
            _dbContext.Seats.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Seat> FindById(int id)
        {
            try
            {
                return await _dbContext.Seats.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Seat>> GetAll()
        {
            try
            {
                return await _dbContext.Seats.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Seat entity)
        {
            _dbContext.Seats.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
