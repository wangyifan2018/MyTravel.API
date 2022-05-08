using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTravel.API.Dtos;
using MyTravel.API.Services;
using System;
using System.Linq;
using AutoMapper;

namespace MyTravel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper
            )
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
    }

        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var tuoristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes();
            if(tuoristRoutesFromRepo == null || tuoristRoutesFromRepo.Count() <=0)
            {
                return NotFound("没有旅游路线");
            }
            var touristRouteDto = _mapper.Map<IEquatable<TouristRouteDto>>(tuoristRoutesFromRepo);
            return Ok(touristRouteDto);
        }

        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            if(touristRouteFromRepo == null)
            {
                return NotFound($"旅游路线{touristRouteId}找不到");
            }
            //var touristRouteDto = new TouristRouteDto()
            //{
            //    Id = touristRouteFromRepo.Id,
            //    Title = touristRouteFromRepo.Title,
            //    Description = touristRouteFromRepo.Description,
            //    Price = touristRouteFromRepo.OriginalPrice * (decimal)(touristRouteFromRepo.DiscountPresent ?? 1),
            //    CreateTime = touristRouteFromRepo.CreateTime,
            //    UpdateTime = touristRouteFromRepo.UpdateTime,
            //    Features = touristRouteFromRepo.Features,
            //    Fees = touristRouteFromRepo.Fees,
            //    Notes = touristRouteFromRepo.Notes,
            //    Rating = touristRouteFromRepo.Rating,
            //    TravelDays = touristRouteFromRepo.TravelDays.ToString(),
            //    TripType = touristRouteFromRepo.TripType.ToString(),
            //    DepartureCity = touristRouteFromRepo.DepartureCity.ToString()
            //};
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
            return Ok(touristRouteDto) ;
        }
    }
}
