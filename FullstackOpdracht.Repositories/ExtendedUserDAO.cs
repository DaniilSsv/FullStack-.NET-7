using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.IDAO;
using Microsoft.EntityFrameworkCore;

namespace FullstackOpdracht.Repositories
{
    public class ExtendedUserDAO : UserDAO
    {
        private readonly VoetbalDbContext _db;

        public ExtendedUserDAO(VoetbalDbContext voetbalDbContext) : base(voetbalDbContext)
        {
            _db = voetbalDbContext;
        }

        public async Task<AspNetUser> FindById(string id)
        {
            try
            {
                return await _db.AspNetUsers.FirstAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
