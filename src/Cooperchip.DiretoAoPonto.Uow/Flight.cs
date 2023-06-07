using Cooperchip.DiretoAoPonto.Uow.Domain.Base;
using System.Collections.Generic;

namespace Cooperchip.DiretoAoPonto.Uow.Domain
{
    public class Flight : EntityBase
    {
        public Flight()
        {
            Passengers = new List<Passenger>();   
        }

        public string? Code { get; set; }
        public string? RoadMap { get; set; }
        public int? Capacity { get; set; } = 0;
        public int? Availability { get; set; }

        public ICollection<Passenger> Passengers { get; set; }

        public void DecreasesAvailability()
        {
            Availability -= 1;
        }

        public bool HasAvailability()
        {
            return Availability > 0;
        }
    }
}
