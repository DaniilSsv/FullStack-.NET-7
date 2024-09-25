using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedSectionDAO : SectionDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedSectionDAO(VoetbalDbContext voetbalDbContext) : base(voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<IEnumerable<Section>> GetSectionsByRing(int id)
        {
            try
            {
                return await _db.Sections
                    .Include(s => s.Ring)
                    .Where(s => s.RingId == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Section>> GetSectionsByStadium(int id)
        {
            try
            {
                return await _db.Sections
                    .Include(s => s.Ring)
                    .Where(s => s.Ring.Stadium.Id == id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
