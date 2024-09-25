using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services.Interfaces;

namespace FullstackOpdracht.Services.IService
{
    public class TicketService : IService<Ticket>
    {
        private IDAO<Ticket> _ticket;
        public TicketService(IDAO<Ticket> dao)
        {
            _ticket = dao;
        }
        public async Task Add(Ticket entity)
        {
            await _ticket.Add(entity);
        }

        public async Task Delete(Ticket entity)
        {
            await _ticket.Delete(entity);
        }

        public async Task<Ticket> FindById(int id)
        {
            return await _ticket.FindById(id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _ticket.GetAll();
        }

        public async Task Update(Ticket entity)
        {
            await _ticket.Update(entity);
        }
    }
}
