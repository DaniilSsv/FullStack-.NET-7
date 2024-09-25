using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class UserDAO : IDAO<AspNetUser>
    {
        private VoetbalDbContext _dbContext;

        public UserDAO(VoetbalDbContext context)
        {
            _dbContext = context;
        }

        public Task Add(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AspNetUser> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AspNetUser>> GetAll()
        {
            try
            {
                return await _dbContext.AspNetUsers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task Update(AspNetUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
