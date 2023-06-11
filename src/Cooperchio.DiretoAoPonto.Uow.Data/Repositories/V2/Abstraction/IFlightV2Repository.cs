namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction
{
    public interface IFlightV2Repository
    {
        Task DecreasePassengers(Guid? flightId);
    }
}
