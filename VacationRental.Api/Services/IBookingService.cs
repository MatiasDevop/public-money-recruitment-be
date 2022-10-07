using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Domain;

namespace VacationRental.Api.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetById(int id);
        Task<Booking> CreateBooking(Booking booking);
    }
}
