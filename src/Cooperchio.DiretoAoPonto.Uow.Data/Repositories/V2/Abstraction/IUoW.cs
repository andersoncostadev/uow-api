namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction
{
    public interface IUoW
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
