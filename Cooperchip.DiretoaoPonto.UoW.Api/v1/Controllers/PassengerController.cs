using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers.v1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/passengers")]
    public class PassengerFailedController : MainController
    {
        private readonly IPassengerFailedRepository _repoPassagenr;
        private readonly IFlightFailedRepository _repoFlight;
        private readonly IMapper _mapper;

        public PassengerFailedController(IPassengerFailedRepository repoPassagenr, IFlightFailedRepository repoFlight, IMapper mapper)
        {
            _repoPassagenr = repoPassagenr;
            _repoFlight = repoFlight;
            _mapper = mapper;
        }

        [HttpPost("add-passenger")]
        [ProducesResponseType(typeof(PassengerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPassenger([FromBody] PassengerDTO passenger)
        {
            if (!ModelState.IsValid)
                return BadRequest("O modelo está inválido");

            var passengerModel = new Passenger
            {
                Name = passenger.Name,
                FlightId = passenger.FlightId
            };

            try
            {
                await _repoPassagenr.AddToFlight(passengerModel);
                await _repoFlight.DecreaseVacancy(passenger.FlightId);

                return CreatedAtAction(nameof(AddPassenger), _mapper.Map<PassengerDTO>(passengerModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
