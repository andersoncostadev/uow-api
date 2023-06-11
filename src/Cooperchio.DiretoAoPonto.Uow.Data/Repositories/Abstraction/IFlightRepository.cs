using Cooperchip.DiretoAoPonto.Uow.Domain;
using System.Linq.Expressions;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction
{
    public interface IFlightRepository : IUnitOfWork
    {
        Task DecreaseVacancy(Guid? flightId);
        Task UpdateFlight(Flight flight);
        Task<Flight> SelectById(Guid? id);
        Task<IEnumerable<Flight>> SelectAll(Expression<Func<Flight, bool>> when = null!);
        Task Create(Flight flight);
    }
}
