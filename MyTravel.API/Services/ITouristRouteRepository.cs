﻿using MyTravel.API.Models;
using System;
using System.Collections.Generic;

namespace MyTravel.API.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouristRoute(Guid touristRouteId);

    }
}
