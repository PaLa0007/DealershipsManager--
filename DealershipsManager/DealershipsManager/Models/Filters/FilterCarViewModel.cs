using DealershipsManager.Data.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Models.Filters
{
    public class FilterCarViewModel
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public TypeEnum Type { get; set; }

        public int Horsepower { get; set; }

        public TransmissionEnum Transmission { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }
    }
}
