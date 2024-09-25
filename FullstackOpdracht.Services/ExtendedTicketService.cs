using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;

namespace FullstackOpdracht.Services
{
    public class ExtendedTicketService : TicketService
    {
        private ExtendedTicketDAO _ticketDAO;

        public ExtendedTicketService(ExtendedTicketDAO dao) : base(dao)
        {
            _ticketDAO = dao;
        }

        public async Task<Ticket> GetLast()
        {
           return await _ticketDAO.GetLast();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsByUserIdAsync(string id)
        {
           return await _ticketDAO.GetAllTicketsByUserIdAsync(id);
        }
    }
}
