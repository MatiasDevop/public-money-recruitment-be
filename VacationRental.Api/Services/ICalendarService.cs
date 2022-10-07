using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Domain;

namespace VacationRental.Api.Services
{
    public interface ICalendarService
    {
        Task<CalendarViewModel> GetAllBookings(int rentalId, DateTime start, int nights);
    }
}
