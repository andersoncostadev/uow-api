using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers.v2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/passengers")]
    public class PassengerController : MainController
    {
        private readonly IPassagenrRepository _repoPassagenr;
        private readonly IFlightRepository _repoFlight;
        private readonly IMapper _mapper;

        public PassengerController(IPassagenrRepository repoPassagenr, IFlightRepository repoFlight, IMapper mapper)
        {
            _repoPassagenr = repoPassagenr;
            _repoFlight = repoFlight;
            _mapper = mapper;
        }

        [HttpPost("add-passenger")]
        [ProducesResponseType(typeof(PassengerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPassenger([FromBody] PassengerRequest passenger)
        {
            if (!ModelState.IsValid)
                return BadRequest("The model is invalid");

            var passengerModel = new Passenger
            {
                Name = passenger.Name,
                FlightId = passenger.FlightId
            };

            try
            {
                await _repoPassagenr.AddToFlight(passengerModel);
                await _repoFlight.DecreaseVacancy(passenger.FlightId);

                var transaction = await _repoPassagenr.Commit();

                return CreatedAtAction(nameof(AddPassenger), _mapper.Map<PassengerDTO>(passengerModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
