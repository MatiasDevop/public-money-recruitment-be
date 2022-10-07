using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Domain
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RentalId { get; set; }
        public virtual Rental Rental { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int Unit { get; set; }
        public int PreparationTime { get; set; }
    }
}
