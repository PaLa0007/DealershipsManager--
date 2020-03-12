using DealershipsManager.Data.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealershipsManager.Data.Entities
{
    public class Car
    {
        public int CarId { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public TypeEnum Type { get; set; }

        public EngineEnum Engine { get; set; }

        public int Horsepower { get; set; }

        public TransmissionEnum Transmission { get; set; }

        public byte Gears { get; set; }

        public string Color { get; set; }

        public double Price { get; set; }

        public bool IsSold { get; set; }



        public int DealershipId { get; set; }

        public virtual Dealership Dealership { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
