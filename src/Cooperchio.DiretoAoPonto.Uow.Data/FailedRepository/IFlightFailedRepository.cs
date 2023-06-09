namespace Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository
{
    public interface IFlightFailedRepository
    {
        Task DecreaseVacancy(Guid? flightId);
    }
}
