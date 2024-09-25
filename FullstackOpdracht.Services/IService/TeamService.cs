using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class TeamService : IService<Team>
    {
        private IDAO<Team> _team;
        public TeamService(IDAO<Team> dao)
        {
            _team = dao;
        }
        public async Task Add(Team entity)
        {
            await _team.Add(entity);
        }

        public Task Delete(Team entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> FindById(int id)
        {
            return await _team.FindById(id);
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _team.GetAll();
        }

        public Task Update(Team entity)
        {
            throw new NotImplementedException();
        }
    }
}
