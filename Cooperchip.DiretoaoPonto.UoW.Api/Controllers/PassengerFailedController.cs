using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Data.FailedRepository;
using Cooperchip.DiretoAoPonto.Uow.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Controllers
{
    [Route("api/passengerfailed")]
    [ApiController]
    public class PassengerFailedController : Controller
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
        public async Task<ActionResult<PassengerDTO>> AddPassenger(PassengerRequest passenger)
        {
            if(!ModelState.IsValid)
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
