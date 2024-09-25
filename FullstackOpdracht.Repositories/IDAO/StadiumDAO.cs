using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class StadiumDAO : IDAO<Stadium>
    {
        private readonly VoetbalDbContext _dbContext;
        public StadiumDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(Stadium entity)
        {
            try
            {
                await _dbContext.Stadia.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Stadium entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Stadium> FindById(int id)
        {
            try
            {
                return await _dbContext.Stadia.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Stadium>> GetAll()
        {
            try
            {
                return await _dbContext.Stadia.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Stadium entity)
        {
            _dbContext.Stadia.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
