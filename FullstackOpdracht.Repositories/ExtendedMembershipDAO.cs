using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedMembershipDAO : MembershipDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedMembershipDAO(VoetbalDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Membership>> GetAllMembershipsByUserIdAsync(string userId)
        {
            try
            {
                return await _db.Memberships
                    .Where(t => t.BookingMemberships.Any(bt => bt.Booking.UserId == userId))
                    .Include(t => t.Team)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Membership> GetLast()
        {
            try
            {
                return await _db.Memberships.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}
