using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;

namespace FullstackOpdracht.Services
{
    public class ExtendedSectionService : SectionService
    {
        private ExtendedSectionDAO _sectionDAO;
        public ExtendedSectionService(ExtendedSectionDAO dao) : base(dao)
        {
            _sectionDAO = dao;
        }

        public async Task<IEnumerable<Section>> GetSectionsByStadium(int id)
        {
            return await _sectionDAO.GetSectionsByStadium(id);
        }

        public async Task<IEnumerable<Section>> GetSectionsByRing(int id)
        {
            return await _sectionDAO.GetSectionsByRing(id);
        }
    }
}
