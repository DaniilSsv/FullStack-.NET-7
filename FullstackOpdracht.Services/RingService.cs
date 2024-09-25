using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using System.IO;

namespace FullstackOpdracht.Services
{
    public class RingService
    {
        private readonly RingDAO _ringDAO;

        public RingService(RingDAO ringDAO)
        {
            _ringDAO = ringDAO;
        }

        public async Task<IEnumerable<Ring>> GetRingsByStadium(int matchId)
        {
            return await _ringDAO.GetRingsByStadium(matchId);
        }

        public async Task<Ring> FindById(int id)
        {
            return await _ringDAO.FindById(id);
        }
    }
}
