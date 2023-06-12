using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers.v3.Controllers
{
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/flights")]
    public class FlightController : MainController
    {
        private readonly IFlightRepository _repoFlight;
        private readonly IPassagenrRepository _repoPassagenr;
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository repoFlight, IPassagenrRepository repoPassagenr, IMapper mapper)
        {
            _repoFlight = repoFlight;
            _repoPassagenr = repoPassagenr;
            _mapper = mapper;
        }

        [HttpGet("get-flights")]
        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _repoFlight.SelectAll();
        }

        [HttpGet("reset-flights")]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ResetFlights(Guid id)
        {
            id = Guid.Parse("a40d5298-bfa0-4e06-8fab-80856d4a1db5");
            var transaction = false;
            var flight = await _repoFlight.SelectById(id);
            if (flight == null)
            {
                var flightDTO = new FlightDTO
                {
                    Id = id,
                    Capacity = 4,
                    Availability = 4,
                    Code = "101 - SaoPaulo/Miami",
                    RoadMap = "Saida as 10:34h. Horario de Brasilia"
                };

                await _repoFlight.Create(_mapper.Map<Flight>(flightDTO));
                transaction = await _repoFlight.Commit();
                return CreatedAtAction(nameof(ResetFlights), flightDTO);
            }

            flight.Id = id;
            flight.Capacity = 4;
            flight.Availability = 4;

            await _repoPassagenr.RemoveFromFlight(id);
            await _repoFlight.UpdateFlight(_mapper.Map<Flight>(flight));
            transaction = await _repoFlight.Commit();
            return Ok(flight);
        }

        [HttpPost("create-flight")]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFlight([FromBody] FlightDTO flight)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model is invalid");

            flight.Id = Guid.Parse("a40d5298-bfa0-4e06-8fab-80856d4a1db5");

            try
            {
                await _repoFlight.Create(_mapper.Map<Flight>(flight));
                var transaction = await _repoFlight.Commit();
                return CreatedAtAction(nameof(AddFlight), flight);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
