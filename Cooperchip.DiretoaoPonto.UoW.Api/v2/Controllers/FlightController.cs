using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Configuration.Settings;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers.v2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/flights")]
    public class FlightController : MainController
    {
        private readonly FlightSettings _settings;
        private readonly IFlightRepository _repoFlight;
        private readonly IPassagenrRepository _repoPassagenr;
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository repoFlight, IPassagenrRepository repoPassagenr, IMapper mapper, IOptions<FlightSettings> settings)
        {
            _repoFlight = repoFlight;
            _repoPassagenr = repoPassagenr;
            _mapper = mapper;
            _settings = settings.Value;
        }

        [HttpGet("get-flights")]
        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _repoFlight.SelectAll();
        }

        [HttpPost("create-flight-appsettings")]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFlightAppSettings()
        {
            if (!ModelState.IsValid)
                return BadRequest("The model is invalid");

            var flight = new Flight()
            {
                Id = _settings.Id,
                Capacity = _settings.Capacity,
                Availability = _settings.Availability,
                Code = _settings.Code,
                RoadMap = _settings.RoadMap,
                Passengers = new List<Passenger>()
            };

            try
            {
                await _repoFlight.Create(flight);
                var transaction = await _repoFlight.Commit();
                return CreatedAtAction(nameof(AddFlightAppSettings), flight);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
