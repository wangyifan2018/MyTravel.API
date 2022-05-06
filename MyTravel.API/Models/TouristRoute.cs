﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MyTravel.API.Models
{
    public class TouristRoute
    {
        public Guid Id { get; set; }
        public string Title { get; internal set; }
        public string Description { get; set; }
        public Decimal OriginalPrice { get; set; }
        public double? DiscountPresent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string Fees { get; set; }
        public string Notes { get; set; }
        public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
        public string Features { get; internal set; }
    }

}
