using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedBookingDAO : BookingDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedBookingDAO(VoetbalDbContext voetbalDbContext) : base(voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<Booking> GetLast()
        {
            try
            {
                return await _db.Bookings.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
