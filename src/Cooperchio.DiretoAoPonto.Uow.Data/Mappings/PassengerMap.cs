using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cooperchip.DiretoAoPonto.Uow.Data.Mappings
{
    public class PassengerMap : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            builder.Property(p => p.FlightId)
                .IsRequired();

            builder.HasOne(p => p.Flight)
                .WithMany(f => f.Passengers)
                .HasForeignKey(f => f.FlightId);
            builder.ToTable("Passenger");
        }
    }
}
