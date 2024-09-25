using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullstackOpdracht.Services
{
    public class ExtendedBookingService : BookingService
    {
        private ExtendedBookingDAO _bookingDAO;

        public ExtendedBookingService(ExtendedBookingDAO dao) : base(dao)
        {
            _bookingDAO = dao;
        }

        public async Task<Booking> GetLast()
        {
            return await _bookingDAO.GetLast();
        }

    }
}
