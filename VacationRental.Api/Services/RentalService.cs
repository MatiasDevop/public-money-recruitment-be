using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Exceptions;
using VacationRental.Api.Models;
using VacationRental.Domain;
using VacationRental.Infrastructure;

namespace VacationRental.Api.Services
{
    public class RentalService : IRentalService
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly IMapper _mapper;
        public RentalService(ApplicationDbContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Rental> CreateRental(Rental rental)
        {
            if(rental == null)
                throw new ApplicationException("null is not allowed");

            await _dataContext.Rentals.AddAsync(rental);
            await _dataContext.SaveChangesAsync();

            return rental;
        }

        public async Task<Rental> GetById(int rentalId)
        {
            var rental = await _dataContext.Rentals.FindAsync(rentalId);

            if (rental == null)
                throw new NotFoundException("Rental", rental.RentalId);

            return rental; 
        }

        public async Task<int> UpdateRental(int id, RentalBindingModel rentalUpdate)
        {
            var existingRental = await _dataContext.Rentals.FindAsync(id);
            var bookings = await _dataContext.Bookings.Where(x => x.RentalId == id).ToListAsync();

            if(existingRental == null)
                throw new NotFoundException("Rental", existingRental.RentalId);

            if (bookings == null || bookings.Count == 0)
            {
                _mapper.Map(rentalUpdate, existingRental);

                _dataContext.Rentals.Update(existingRental);
            }
            else
            {
                if (bookings.Any(x => x.Unit > rentalUpdate.Units))
                    throw new ApplicationException("process is not allowed");

                existingRental.PreparationTimeInDays = rentalUpdate.PreparationTimeInDays;
                _dataContext.Rentals.Update(existingRental);
            }
            
            await _dataContext.SaveChangesAsync();

            return id;
        }
    }
}
