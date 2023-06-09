using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cooperchio.DiretoAoPonto.Uow.Data.Orm
{
    public class UoWDbContext : DbContext
    {
        public UoWDbContext(DbContextOptions<UoWDbContext> options) : base(options){}

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //onde não tiver setado varchar e a propriedade for string, seta varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                               e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                //property.Relational().ColumnType = "varchar(100)";
                property.SetColumnType("varchar(100)");
            }

            //Todo: Busca os Mappings de uma só vez
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UoWDbContext).Assembly);

            //Todo: remover exclusão em casacata
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
