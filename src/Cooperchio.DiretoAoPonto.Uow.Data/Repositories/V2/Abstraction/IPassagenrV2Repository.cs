using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction
{
    public interface IPassagenrV2Repository
    {
        Task AddPassenger(Passenger passenger);
    }
}
