using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;

namespace FullstackOpdracht.Services
{
    public class ExtendedSeatService : SeatService
    {
        private ExtendedSeatDAO _seatDAO;

        public ExtendedSeatService(ExtendedSeatDAO dao) : base(dao)
        {
            _seatDAO = dao;
        }


        public async Task<List<Seat>> GetFreeSeatsInSectionAsync(int id)
        {
            return await _seatDAO.GetFreeSeatsInSectionAsync(id);
        }

        public async Task<List<Seat>> GetSeatsByStadium(int id)
        {
            return await _seatDAO.GetSeatsByStadium(id);
        }


    }
}
