using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class TeamDAO : IDAO<Team>
    {
        private readonly VoetbalDbContext _dbContext;
        public TeamDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(Team entity)
        {
            try
            {
                await _dbContext.Teams.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Team entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> FindById(int id)
        {
            try
            {
                return await _dbContext.Teams.Where(t => t.Id == id)
                    .Include(t => t.Stadium)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            try
            {
                return await _dbContext.Teams.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task Update(Team entity)
        {
            throw new NotImplementedException();
        }
    }
}
