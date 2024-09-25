using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;

namespace FullstackOpdracht.Services
{
    public class ExtendedBookingTicketService : BookingTicketService
    {
        private ExtendedBookingTicketDAO _bookingTicketDAO;

        public ExtendedBookingTicketService(ExtendedBookingTicketDAO dao) : base(dao)
        {
            _bookingTicketDAO = dao;
        }

        public async Task<BookingTicket> FindBooking(int ticketId)
        {
            return await _bookingTicketDAO.FindBooking(ticketId);
        }
    }
}
