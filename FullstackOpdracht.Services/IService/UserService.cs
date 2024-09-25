using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Services.IService
{
    public class UserService : IService<AspNetUser>
    {
        private IDAO<AspNetUser> _user;
        public UserService(IDAO<AspNetUser> dao)
        {
            _user = dao;
        }

        public Task Add(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AspNetUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<AspNetUser> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AspNetUser>> GetAll()
        {
            return await _user.GetAll();
        }

        public Task Update(AspNetUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
