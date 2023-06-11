using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation
{
    public class FlightRepository : IFlightRepository
    {
        private readonly UoWDbContext _context;

        public FlightRepository(UoWDbContext context) => _context = context;
       
        public async Task Create(Flight flight)
        {
            var flightExisting = await _context.Set<Flight>().FindAsync(flight.Id);
           
            if (flightExisting == null)
                await _context.Set<Flight>().AddAsync(flight);
        }

        public async Task DecreaseVacancy(Guid? flightId)
        {
            if (flightId == null)
                throw new Exception("Flight ID cannot be null");

            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null)
                throw new Exception("Flight not found");

            if (!flight.HasAvailability())
                throw new Exception("Flight has no more seats available!");

            flight.DecreasesAvailability();

            _context.Set<Flight>().Update(flight);
        }

        public async Task<IEnumerable<Flight>> SelectAll(Expression<Func<Flight, bool>> when = null!)
        {
            if( when == null)
            {
                return await _context.Set<Flight>().Include(p => p.Passengers).AsNoTracking().ToListAsync();
            }
            return await _context.Set<Flight>().Include(p => p.Passengers).AsNoTracking().Where(when).ToListAsync();
        }

        public async Task<Flight> SelectById(Guid? id) => await _context.Set<Flight>().FindAsync(id);

        public async Task UpdateFlight(Flight flight)
        {
            _context.Set<Flight>().Update(flight);
            await Task.CompletedTask;
        }
        public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

        public Task Rollback() => Task.CompletedTask;
                
    }
}
