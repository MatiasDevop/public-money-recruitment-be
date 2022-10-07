using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Domain;
using VacationRental.Infrastructure;

namespace VacationRental.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _dataContext;

        public BookingService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Booking> CreateBooking(Booking booking)
        {
            var bookExisting = await _dataContext.Bookings.FindAsync(booking.BookingId);
            var rentals = await _dataContext.Rentals.ToListAsync();
            var bookings = await _dataContext.Bookings.ToListAsync();

            if (bookExisting != null)
                throw new ApplicationException("item already created");

            if (booking.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");

            if (booking.Unit <= 0)
                throw new ApplicationException("Unit must be positive");
            
            if (!rentals.Exists(x => x.RentalId == booking.RentalId))
                throw new ApplicationException("Rental not found");

            if (bookings.Exists(b => b.Unit == booking.Unit))
                throw new ApplicationException("Unit already been occupied! check out another room");
            
            if (booking.Unit > rentals.Find(r => r.RentalId == booking.RentalId).Units)
                throw new ApplicationException("Unit not found try out another unit please");

            for (int i = 0; i < booking.Nights; i++)
            {
                var count = 0;
                foreach (var item in bookings)
                {
                    if (item.RentalId == booking.RentalId
                        && (item.Start <= booking.Start.Date && item.Start.AddDays(item.Nights) > booking.Start.Date)
                        || (item.Start < booking.Start.AddDays(booking.Nights) && item.Start.AddDays(item.Nights) >= booking.Start.AddDays(booking.Nights))
                        || (item.Start > booking.Start && item.Start.AddDays(item.Nights) < booking.Start.AddDays(booking.Nights)))
                    {
                        count++;
                    }
                }
                if(count >= rentals.Find(x => x.RentalId == booking.RentalId).Units)
                    throw new ApplicationException("Not available units");
            }

            await _dataContext.Bookings.AddAsync(booking);
            await _dataContext.SaveChangesAsync();

            return booking;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _dataContext.Bookings.Include(x => x.Rental).ToListAsync();
        }

        public async Task<Booking> GetById(int id)
        {
            return await _dataContext.Bookings.
                Include(x => x.Rental)
                .SingleOrDefaultAsync(x => x.BookingId == id);
        }
    }
}
