using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Api.Services;
using VacationRental.Domain;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookinService;
        private readonly IMapper _mapper;
        public BookingsController(IBookingService bookinService, IMapper mapper)
        {
            _bookinService = bookinService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<BookingViewModel> GetBooking(int bookingId)
        {
            var result = await _bookinService.GetById(bookingId);
            
            return _mapper.Map<BookingViewModel>(result);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(BookingBindingModel model)
        {
            var newBook = new Booking
            {
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start,
                Unit = model.Unit,
                PreparationTime = model.PreparationTime
            };
            var result = await _bookinService.CreateBooking(newBook);

            return new ResourceIdViewModel { Id = result.BookingId };
        }
    }
}
