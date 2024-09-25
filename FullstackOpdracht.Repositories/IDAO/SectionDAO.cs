using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Repositories.IDAO
{
    public class SectionDAO : IDAO<Section>
    {
        private readonly VoetbalDbContext _dbContext;

        public SectionDAO(VoetbalDbContext dbContext)
        {
            _dbContext = dbContext;
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
            try
            {
                return await _dbContext.Sections.Where(s => s.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in dao: " + ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Section>> GetAll()
        {
            try
            {
                return await _dbContext.Sections.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error in dao: "+ex.Message);
                throw ex;
            }
        }

        public Task Update(Section entity)
        {
            throw new NotImplementedException();
        }
    }
}
