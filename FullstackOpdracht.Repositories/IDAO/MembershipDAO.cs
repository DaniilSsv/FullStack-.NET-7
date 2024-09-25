using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class MembershipDAO : IDAO<Membership>
    {
        private readonly VoetbalDbContext _dbContext;
        public MembershipDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }
        public async Task Add(Membership entity)
        {
            try
            {
                await _dbContext.Memberships.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Membership entity)
        {
            _dbContext.Memberships.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Membership> FindById(int id)
        {
            try
            {
                return await _dbContext.Memberships.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Membership>> GetAll()
        {
            try
            {
                return await _dbContext.Memberships.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(Membership entity)
        {
            _dbContext.Memberships.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
