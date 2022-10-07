using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Domain
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
 