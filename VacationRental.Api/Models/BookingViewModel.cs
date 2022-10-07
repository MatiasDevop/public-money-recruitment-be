using System;

namespace VacationRental.Api.Models
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int Unit { get; set; }
        public int PreparationTime { get; set; }
    }
}
