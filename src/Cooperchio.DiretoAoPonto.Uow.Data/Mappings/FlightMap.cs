using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Mappings
{
    public class FlightMap : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Code)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnType("varchar");

            builder.Property(f => f.RoadMap)
                .IsRequired()
                .HasMaxLength (100)
                .HasColumnType("varchar(100)");

           
            builder.HasMany(f => f.Passengers)
                .WithOne(p => p.Flight)
                .HasForeignKey(p => p.FlightId);
            builder.ToTable("Flight");
        }
    }   
}
