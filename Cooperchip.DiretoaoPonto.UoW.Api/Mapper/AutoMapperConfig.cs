using AutoMapper;
using Cooperchip.DiretoaoPonto.UoW.Api.Models;
using Cooperchip.DiretoAoPonto.Uow.Domain;

namespace Cooperchip.DiretoaoPonto.UoW.Api.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Passenger, PassengerDTO>().ReverseMap();
        }
    }
}
