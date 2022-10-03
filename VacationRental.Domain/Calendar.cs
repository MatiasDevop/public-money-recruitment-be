using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Domain
{
    public class Calendar
    {
        public int CalendarId { get; set; }
        public int RentalId { get; set; }
        public virtual Rental Rental { get; set; }

        public virtual ICollection<CalendarDate> Dates { get; set; }
    }
}
