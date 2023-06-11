namespace Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Settings
{
    public class FlightSettings
    {
        public const string SessionName = nameof(FlightSettings);

        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? RoadMap { get; set; }
        public int? Capacity { get; set; } = 4;
        public int? Availability { get; set;} = 4;
    }
}
