using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullstackOpdracht.Services.IService
{
    public class SeatService : IService<Seat>
    {
        private IDAO<Seat> _seat;
        public SeatService(IDAO<Seat> dao)
        {
            _seat = dao;
        }
        public async Task Add(Seat entity)
        {
            await _seat.Add(entity);
        }

        public async Task Delete(Seat entity)
        {
            await _seat.Delete(entity);
        }

        public async Task<Seat> FindById(int id)
        {
            return await _seat.FindById(id);
        }

        public async Task<IEnumerable<Seat>> GetAll()
        {
            return await _seat.GetAll();
        }

        public async Task Update(Seat entity)
        {
            await _seat.Update(entity);
        }
    }
}
