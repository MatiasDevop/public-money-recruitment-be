using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Domain;

namespace VacationRental.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            var initializer = new DbInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            SeedBookings(context);
        }
        public void SeedBookings(ApplicationDbContext context)
        {
            var rentals = new[] {
                new Rental { RentalId = 1, Units = 3 },
            };

            context.Rentals.AddRange(rentals);

            var origins = new[] {
                new Booking { BookingId = 1, RentalId = 1, Start = DateTime.Now,  Nights = 2, Unit=1, PreparationTime = 2 },
                new Booking { BookingId = 2, RentalId = 2, Start = DateTime.Now,  Nights = 5, Unit=3, PreparationTime = 3 }
            };

            context.Bookings.AddRange(origins);
            try
            {
                context.SaveChanges();
            }
            catch { ResetContextState(context); }
        }

        private void ResetContextState(ApplicationDbContext context) => context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

    }
}
