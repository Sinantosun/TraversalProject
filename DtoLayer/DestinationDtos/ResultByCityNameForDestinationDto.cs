﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.DestinationDtos
{
    public class ResultByCityNameForDestinationDto
    {
        public int DestinationID { get; set; }
        public string City { get; set; }
        public string DayNight { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string? LongDescription { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
        public DateTime DestinationDate { get; set; }
        public string CoverImage { get; set; }
    }
}
