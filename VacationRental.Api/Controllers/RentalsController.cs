using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Api.Services;
using VacationRental.Domain;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;
        public RentalsController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<RentalViewModel> Get(int rentalId)
        {
            var rental = await _rentalService.GetById(rentalId);
           
            return _mapper.Map<RentalViewModel>(rental);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(RentalBindingModel model)
        {
            var newRental = _mapper.Map<Rental>(model);
           
            var result = await _rentalService.CreateRental(newRental);

            return new ResourceIdViewModel { Id = result.RentalId};
        }

        [HttpPut("id")]
        public async Task<ResourceIdViewModel> Put(int id, RentalBindingModel model)
        {
            var resultId = await _rentalService.UpdateRental(id, model);

            return new ResourceIdViewModel { Id = resultId };
        }
    }
}
