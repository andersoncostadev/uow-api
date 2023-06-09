using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository
{
    public interface IPassengerFailedRepository
    {
        Task AddToFlight(Passenger passenger);
    }
}
