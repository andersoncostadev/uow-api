﻿// <auto-generated />
using System;
using Cooperchio.DiretoAoPonto.Uow.Data.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cooperchip.DiretoAoPonto.Uow.Data.Migrations
{
    [DbContext(typeof(UoWDbContext))]
    partial class UoWDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cooperchip.DiretoAoPonto.Uow.Domain.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("Availability")
                        .HasColumnType("int");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar");

                    b.Property<string>("RoadMap")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Flight", (string)null);
                });

            modelBuilder.Entity("Cooperchip.DiretoAoPonto.Uow.Domain.Passenger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("FlightId")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("Passenger", (string)null);
                });

            modelBuilder.Entity("Cooperchip.DiretoAoPonto.Uow.Domain.Passenger", b =>
                {
                    b.HasOne("Cooperchip.DiretoAoPonto.Uow.Domain.Flight", "Flight")
                        .WithMany("Passengers")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Cooperchip.DiretoAoPonto.Uow.Domain.Flight", b =>
                {
                    b.Navigation("Passengers");
                });
#pragma warning restore 612, 618
        }
    }
}
