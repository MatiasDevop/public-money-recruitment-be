using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Domain;
using VacationRental.Infrastructure;

namespace VacationRental.Api.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ApplicationDbContext _dataContext;

        public CalendarService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CalendarViewModel> GetAllBookings(int rentalId, DateTime start, int nights)
        {
            var bookings = await _dataContext.Bookings.ToListAsync();
            var rentals = await _dataContext.Rentals.ToListAsync();

            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!rentals.Exists(x => x.RentalId == rentalId))
                throw new ApplicationException("Rental not found");

            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>(),
            };

            for (int i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<PreparationTimeViewModel>()
                };

                foreach (var booking in bookings)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.BookingId, Unit = booking.Unit });
                        date.PreparationTimes.Add(new PreparationTimeViewModel { Unit = booking.Unit });
                    }
                }
                result.Dates.Add(date);
            }

            return result;
        }
    }
}
