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
    public class ExtendedUserService : UserService
    {
        private ExtendedUserDAO _userDAO;

        public ExtendedUserService(ExtendedUserDAO dao) : base(dao)
        {
            _userDAO = dao;
        }

        public async Task<AspNetUser> FindById(string id)
        {
            return await _userDAO.FindById(id);
        }

    }
}
