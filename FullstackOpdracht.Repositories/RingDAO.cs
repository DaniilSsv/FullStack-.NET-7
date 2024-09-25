using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class RingDAO
    {
        private readonly VoetbalDbContext _db;

        public RingDAO(VoetbalDbContext voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<IEnumerable<Ring>> GetRingsByStadium(int stadiumId)
        {
            try
            {
                return await _db.Rings
                    .Where(r => r.StadiumId == stadiumId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<Ring> FindById(int id)
        {
            try
            {
                return await _db.Rings.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
