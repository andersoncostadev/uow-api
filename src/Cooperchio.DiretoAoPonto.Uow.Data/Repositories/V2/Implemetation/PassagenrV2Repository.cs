using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Implemetation
{
    public class PassagenrV2Repository : IPassagenrV2Repository
    {
        private readonly UoWDbContext _context;
        public PassagenrV2Repository(UoWDbContext context)
        {
            _context = context;
        }
        public async Task AddPassenger(Passenger passenger) => await _context.Set<Passenger>().AddAsync(passenger);
    }
}
