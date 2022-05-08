using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTravel.API.Services;
using System;
using System.Linq;

namespace MyTravel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }

        [HttpGet]
        public IActionResult GetTouristRoutes()
        {
            var tuoristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes();
            if(tuoristRoutesFromRepo == null || tuoristRoutesFromRepo.Count() <=0)
            {
                return NotFound("没有旅游路线");
            } 
            return Ok(tuoristRoutesFromRepo);
        }

        [HttpGet("{touristRouteId}")]
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            if(touristRouteFromRepo == null)
            {
                return NotFound($"旅游路线{touristRouteId}找不到");
            }

            return Ok(touristRouteFromRepo) ;
        }
    }
}
