using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class BookingService : IService<Booking>
    {
        private IDAO<Booking> _booking;
        public BookingService(IDAO<Booking> dao)
        {
            _booking = dao;
        }
        public async Task Add(Booking entity)
        {
            await _booking.Add(entity);
        }

        public async Task Delete(Booking entity)
        {
            await _booking.Delete(entity);
        }

        public async Task<Booking> FindById(int id)
        {
            return await _booking.FindById(id);
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _booking.GetAll();
        }

        public async Task Update(Booking entity)
        {
            await _booking.Update(entity);
        }
    }
}
