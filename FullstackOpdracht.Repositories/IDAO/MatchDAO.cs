using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class MatchDAO : IDAO<Match>
    {
        private readonly VoetbalDbContext _dbContext;

        public MatchDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }

        public async Task Add(Match entity)
        {
            try
            {
                await _dbContext.Matches.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Match entity)
        {
            try
            {
                _dbContext.Matches.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Match> FindById(int id)
        {
            try
            {
                return await _dbContext.Matches
                    .Include(m => m.AwayTeam)
                    .Include(m => m.HomeTeam)
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Match>> GetAll()
        {
            try
            {
                return await _dbContext.Matches.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Match entity)
        {
            try
            {
                _dbContext.Matches.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
