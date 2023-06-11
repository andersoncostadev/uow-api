using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Implemetation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UoWDbContext _context;
        public UnitOfWork(UoWDbContext context) => _context = context;
        
        public async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;
        public async Task Rollback() => await Task.CompletedTask;
    }
}
