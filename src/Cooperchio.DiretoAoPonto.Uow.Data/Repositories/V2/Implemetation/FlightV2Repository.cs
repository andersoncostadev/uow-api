using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Implemetation
{
    public class FlightV2Repository : IFlightV2Repository
    {
        private readonly UoWDbContext _context;
        public FlightV2Repository(UoWDbContext context)
        {
            _context = context;
        }
        public async Task DecreasePassengers(Guid? flightId)
        {
            if (flightId == null)
                throw new Exception("Flight id cannot be null");

            var flight = await _context.Set<Flight>().FindAsync(flightId);

            if (flight == null)
                throw new Exception("Flight not found");

            if (!flight.HasAvailability())
                throw new Exception("Flight has no passengers");

            flight.DecreasesAvailability();

            _context.Set<Flight>().Update(flight);
        }
    }
}
