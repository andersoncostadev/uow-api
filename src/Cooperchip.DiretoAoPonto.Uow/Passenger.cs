using Cooperchip.DiretoAoPonto.Uow.Domain.Base;
using System;
using System.Text.Json.Serialization;

namespace Cooperchip.DiretoAoPonto.Uow.Domain
{
    public class Passenger : EntityBase
    {
        public Passenger()
        {
            
        }

        public string? Name { get; set; }
        public Guid? FlightId { get; set; }

        [JsonIgnore]
        public Flight? Flight { get; set; }
    }
}