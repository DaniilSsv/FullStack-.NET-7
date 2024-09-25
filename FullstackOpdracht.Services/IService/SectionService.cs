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
    public class SectionService : IService<Section>
    {
        private readonly IDAO<Section> _dao;

        public SectionService(IDAO<Section> dao)
        {
            _dao = dao;
        }

        public Task Add(Section entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Section entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Section> FindById(int id)
        {
            return await _dao.FindById(id);
        }

        public async Task<IEnumerable<Section>> GetAll()
        {
            return await _dao.GetAll();
        }

        public Task Update(Section entity)
        {
            throw new NotImplementedException();
        }
    }
}
