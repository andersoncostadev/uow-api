using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository
{
    public interface IFlightFailedRepository
    {
        Task AddPassenger(Guid? flightId);
    }

    public class FlightFailedRepository : IFlightFailedRepository
    {
        private readonly UoWDbContext _context;
        public FlightFailedRepository(UoWDbContext context)
        {
            _context = context;
        }
        public async Task AddPassenger(Guid? flightId)
        {
            if (flightId == null) 
                throw new Exception("Id do Voo não pode ser nulo");

            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null)
                throw new Exception("Voo não encontrado");

            if(!flight.HasAvailability())
                throw new Exception("Voo não possui mais assentos disponíveis");

            flight.DecreasesAvailability();

            _context.Set<Flight>().Update(flight);
            await _context.SaveChangesAsync();
        }
    }
}
