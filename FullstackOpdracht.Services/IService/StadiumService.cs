using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class StadiumService : IService<Stadium>
    {
        private IDAO<Stadium> _stadium;
        public StadiumService(IDAO<Stadium> dao)
        {
            _stadium = dao;
        }
        public async Task Add(Stadium entity)
        {
            await _stadium.Add(entity);
        }

        public Task Delete(Stadium entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Stadium> FindById(int id)
        {
            return await _stadium.FindById(id);
        }

        public async Task<IEnumerable<Stadium>> GetAll()
        {
            return await _stadium.GetAll();
        }

        public async Task Update(Stadium entity)
        {
           await _stadium.Update(entity);
        }
    }
}
