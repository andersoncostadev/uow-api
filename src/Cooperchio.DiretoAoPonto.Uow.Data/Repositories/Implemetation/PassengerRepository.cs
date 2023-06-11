using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Implemetation
{
    public class PassengerRepository : IPassagenrRepository
    {
        private readonly UoWDbContext _context;
        public PassengerRepository(UoWDbContext context) => _context = context;

        public async Task AddToFlight(Passenger passenger) => await _context.Set<Passenger>().AddAsync(passenger);

        public async Task RemoveFromFlight(Guid flightId)
        {
            var passengers = await _context.Set<Passenger>().AsNoTracking().Where(x => x.FlightId == flightId).ToListAsync();

            if (passengers == null)
                throw new Exception("Passenger not found");

            _context.Set<Passenger>().RemoveRange(passengers);
        }

        public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;

        public async Task Rollback() => await Task.CompletedTask;

    }
}
