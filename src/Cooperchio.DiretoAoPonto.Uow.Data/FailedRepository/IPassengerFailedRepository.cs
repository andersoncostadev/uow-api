using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository
{
    public interface IPassengerFailedRepository
    {
        Task AddToFlight(Passenger passenger);
    }
}

public partial class PassengerFailedRepository : IPassengerFailedRepository
{
    private readonly UoWDbContext _context;
    public PassengerFailedRepository(UoWDbContext context)
    {
        _context = context;
    }
    public async Task AddToFlight(Passenger passenger)
    {
      await _context.Set<Passenger>().AddAsync(passenger);
      await _context.SaveChangesAsync();
    }
}
