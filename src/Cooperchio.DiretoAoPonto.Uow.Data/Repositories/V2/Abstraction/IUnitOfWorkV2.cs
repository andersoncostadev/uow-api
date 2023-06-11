namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction
{
    public interface IUnitOfWorkV2
    {
        Task<bool> Commit();
        Task Rollback();
    }
}
