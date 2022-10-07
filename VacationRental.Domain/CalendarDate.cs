using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Domain
{
    public class CalendarDate
    {
        public int CalendarDateId { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Booking> PreparationTimes { get; set; }
    }
}
