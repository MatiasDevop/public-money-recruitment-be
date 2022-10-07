using AutoMapper;
using VacationRental.Api.Models;
using VacationRental.Domain;

namespace VacationRental.Api.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingViewModel>();
            CreateMap<BookingViewModel, Booking>();
            CreateMap<Rental, RentalViewModel>();
            CreateMap<RentalViewModel, Rental>();
            CreateMap<RentalBindingModel, Rental>();
        }
    }
}
