namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
