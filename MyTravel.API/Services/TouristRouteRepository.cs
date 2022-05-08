using MyTravel.API.Database;
using MyTravel.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTravel.API.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;

        public TouristRouteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TouristRoutePicture> GetPicturesByTouristRouteId(Guid touristRoutuId)
        {
            return _context.TouristRoutePictures
                .Where(p => p.TouristRouteId == touristRoutuId).ToList();
        }

        public TouristRoute GetTouristRoute(Guid touristRouteId)
        {
            return _context.TouristRoutes.FirstOrDefault(n => n.Id == touristRouteId);
        }

        public IEnumerable<TouristRoute> GetTouristRoutes()
        {
           return _context.TouristRoutes;
        }

        public bool TouristRouteExists(Guid touristRouteId)
        {
            return _context.TouristRoutes.Any(t => t.Id == touristRouteId);
        }
        public TouristRoutePicture GetPicture(int pictureId)
        {
            return _context.TouristRoutePictures.Where(p => p.Id == pictureId).FirstOrDefault();
        }
    }
}
