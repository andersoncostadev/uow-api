namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
