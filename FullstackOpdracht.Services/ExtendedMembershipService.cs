using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Services
{
    public class ExtendedMembershipService : MembershipService
    {
        private ExtendedMembershipDAO _membershipDAO;

        public ExtendedMembershipService(ExtendedMembershipDAO membershipDAO) : base(membershipDAO)
        {
            _membershipDAO = membershipDAO;
        }

        public async Task<IEnumerable<Membership>> GetAllMembershipsByUserIdAsync(string userId)
        {
            return await _membershipDAO.GetAllMembershipsByUserIdAsync(userId);
        }

        public async Task<Membership> GetLast()
        {
            return await _membershipDAO.GetLast();
        }
    }
}
