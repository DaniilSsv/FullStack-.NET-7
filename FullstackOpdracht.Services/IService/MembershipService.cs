using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullstackOpdracht.Services.IService
{
    public class MembershipService : IService<Membership>
    {
        private IDAO<Membership> _membership;
        public MembershipService(IDAO<Membership> dao)
        {
            _membership = dao;
        }
        public async Task Add(Membership entity)
        {
            await _membership.Add(entity);
        }

        public async Task Delete(Membership entity)
        {
            await _membership.Delete(entity);
        }

        public async Task<Membership> FindById(int id)
        {
            return await _membership.FindById(id);
        }

        public async Task<IEnumerable<Membership>> GetAll()
        {
            return await _membership.GetAll();
        }

        public async Task Update(Membership entity)
        {
            await _membership.Update(entity);
        }
    }
}
