using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class MatchService : IService<Match>
    {
        private IDAO<Match> _matchDAO;

        public MatchService(IDAO<Match> dao)
        {
            _matchDAO = dao;
        }

        public async Task Add(Match entity)
        {
            await _matchDAO.Add(entity);
        }

        public async Task Delete(Match entity)
        {
            await _matchDAO.Delete(entity);
        }

        public async Task<Match> FindById(int id)
        {
            return await _matchDAO.FindById(id);
        }

        public async Task<IEnumerable<Match>> GetAll()
        {
            return await _matchDAO.GetAll();
        }

        public async Task Update(Match entity)
        {
            await _matchDAO.Update(entity);
        }
    }
}
