using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class BookingTicketService : IService<BookingTicket>
    {
        private IDAO<BookingTicket> _bookingTicket;
        public BookingTicketService(IDAO<BookingTicket> dao)
        {
            _bookingTicket = dao;
        }
        public async Task Add(BookingTicket entity)
        {
            await _bookingTicket.Add(entity);
        }

        public async Task Delete(BookingTicket entity)
        {
            await _bookingTicket.Delete(entity);
        }

        public async Task<BookingTicket> FindById(int id)
        {
            return await _bookingTicket.FindById(id);
        }

        public async Task<IEnumerable<BookingTicket>> GetAll()
        {
            return await _bookingTicket.GetAll();
        }

        public async Task Update(BookingTicket entity)
        {
            await _bookingTicket.Update(entity);
        }
    }
}