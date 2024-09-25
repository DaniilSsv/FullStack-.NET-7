using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class TicketDAO : IDAO<Ticket>
    {
        private readonly VoetbalDbContext _dbContext;
        public TicketDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }

        public async Task Add(Ticket entity)
        {
            try
            {
                await _dbContext.Tickets.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Ticket entity)
        {
            _dbContext.Tickets.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> FindById(int id)
        {
            try
            {
                return await _dbContext.Tickets.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            try
            {
                return await _dbContext.Tickets.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Ticket entity)
        {
            _dbContext.Tickets.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
