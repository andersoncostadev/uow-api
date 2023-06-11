using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction
{
    public interface IPassagenrRepository : IUnitOfWork
    {
        Task AddToFlight(Passenger passenger);
        Task RemoveFromFlight(Guid flightId);
    }
}
