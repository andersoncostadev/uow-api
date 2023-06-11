using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Data.Repositories.V2.Abstraction;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [Route("api/v{version:apiVersion}/passenger-V2")]
    public class PassengerV2Controller : ControllerBase
    {
        private readonly IUnitOfWorkV2 _uow;
        private readonly IPassagenrRepository _repoPassagenr;
        private readonly IFlightRepository _repoFlight;
        private readonly IMapper _mapper;

        public PassengerV2Controller(IPassagenrRepository repoPassagenr, IFlightRepository repoFlight, IMapper mapper, IUnitOfWorkV2 uow)
        {
            _repoPassagenr = repoPassagenr;
            _repoFlight = repoFlight;
            _mapper = mapper;
            _uow = uow;
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

                var transaction = await _uow.Commit();

                return CreatedAtAction(nameof(AddPassenger), _mapper.Map<PassengerDTO>(passengerModel));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex.Message);
            }
        }
    }
}
