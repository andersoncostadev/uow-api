using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Domain;

public class PassengerFailedRepository : IPassengerFailedRepository
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
