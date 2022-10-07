using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Domain;

namespace VacationRental.Api.Services
{
    public interface IRentalService
    {
        Task<Rental> GetById(int id);
        Task<Rental> CreateRental(Rental rental);
        Task<int> UpdateRental(int id, RentalBindingModel rentalUpdate);
    }
}
