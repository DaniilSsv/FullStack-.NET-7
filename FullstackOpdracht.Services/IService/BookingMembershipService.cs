using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class BookingMembershipService : IService<BookingMembership>
    {
        private IDAO<BookingMembership> _bookingMembership;
        public BookingMembershipService(IDAO<BookingMembership> dao)
        {
            _bookingMembership = dao;
        }
        public async Task Add(BookingMembership entity)
        {
            await _bookingMembership.Add(entity);
        }

        public async Task Delete(BookingMembership entity)
        {
            await _bookingMembership.Delete(entity);
        }

        public async Task<BookingMembership> FindById(int id)
        {
            return await _bookingMembership.FindById(id);
        }

        public async Task<IEnumerable<BookingMembership>> GetAll()
        {
            return await _bookingMembership.GetAll();
        }

        public async Task Update(BookingMembership entity)
        {
            await _bookingMembership.Update(entity);
        }
    }
}
